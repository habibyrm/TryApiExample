using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using stajapi.Helpers;
using stajapi.Login;
using stajapi.Services;

namespace stajapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (_authService.ValidateUser(loginDto))
            {
                var result = _authService.GenerateJwtToken(loginDto.Username);
                if (result != null)
                {
                    return Ok("Token başarıyla oluşturuldu.");
                }
                else
                    return BadRequest("Token oluşturulamadı.");
            }
            else
                return Unauthorized("Kullanıcı adı veya şifre hatalı.");
        }
    }
}
