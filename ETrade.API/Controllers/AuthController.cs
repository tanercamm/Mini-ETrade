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

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                 ?? User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if (userId == null)
                return Unauthorized("Kimlik doğrulama hatası.");

            if (!Guid.TryParse(userId, out var guidUserId))
                return BadRequest("Geçersiz kullanıcı ID formatı.");

            var user = await _authService.GetUserByIdAsync(guidUserId);
            if (user == null)
                return NotFound("Kullanıcı bulunamadı.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            if (token == null) return Unauthorized("Geçersiz giriş.");

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            if (!result) return BadRequest("Kullanıcı kaydı başarısız.");

            return Ok("Kullanıcı başarıyla kaydedildi.");
        }
    }
}