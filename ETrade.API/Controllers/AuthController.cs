using ETrade.Application.DTOs.User;
using ETrade.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace ETrade.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("assign-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole(AssignRoleDTO assignRoleDto)
        {
            var result = await _authService.AssignRoleAsync(assignRoleDto);
            return result ? Ok("Role assigned.") : BadRequest("The role could not be assigned.");
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var tokenUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if (tokenUserId == null)
                return Unauthorized("Authentication error.");

            if (!Guid.TryParse(tokenUserId, out var guidTokenUserId))
                return BadRequest("Invalid user ID format.");

            var isAdmin = User.IsInRole("Admin");

            var user = await _authService.GetUserByIdAsync(id, guidTokenUserId, isAdmin);
            if (user == null)
                return Forbid("You are not authorized to access this user's information.");

            return Ok(user);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            if (token == null) return Unauthorized("Invalid entry.");

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            if (!result) return BadRequest("User registration failed.");

            return Ok("The user has been successfully registered.");
        }
    }
}