﻿using ETrade.Application.DTOs.User;
using ETrade.Application.Services.Abstract;
using ETrade.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ETrade.Application.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<UserDTO?> GetUserByIdAsync(Guid requestedUserId, Guid requesterUserId, bool isRequesterAdmin)
        {
            if (requestedUserId != requesterUserId && !isRequesterAdmin)
                return null;

            var user = await _userManager.FindByIdAsync(requestedUserId.ToString());
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? "Customer"
            };
        }


        public async Task<string?> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
                return null;

            var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
            if (!result.Succeeded)
                return null;

            return await GenerateJwtToken(user);
        }

        public async Task<bool> RegisterAsync(RegisterDTO registerDTO)
        {
            if (string.IsNullOrWhiteSpace(registerDTO.Email) || string.IsNullOrWhiteSpace(registerDTO.Password))
                throw new ArgumentException("Email and password cannot be blank.");

            var user = new User
            {
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                FullName = registerDTO.FullName
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
                return false;

            var roleResult = await _userManager.AddToRoleAsync(user, "Customer");

            if (!roleResult.Succeeded)
            {
                var roleErrors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                throw new Exception($"Role could not be assigned: {roleErrors}");
            }

            return true;
        }

        public async Task<bool> AssignRoleAsync(AssignRoleDTO assignRoleDto)
        {
            var user = await _userManager.FindByIdAsync(assignRoleDto.UserId.ToString());
            if (user == null) return false;

            var roleExists = await _userManager.IsInRoleAsync(user, assignRoleDto.Role);
            if (roleExists) return false;

            var result = await _userManager.AddToRoleAsync(user, assignRoleDto.Role);
            return result.Succeeded;
        }


        private async Task<string> GenerateJwtToken(User user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var secret = _configuration["JwtSettings:Secret"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            if (string.IsNullOrEmpty(secret))
                throw new ArgumentNullException("JwtSettings:Secret", "JWT secret key is missing or empty.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,              
                audience: audience,          
                claims: authClaims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
