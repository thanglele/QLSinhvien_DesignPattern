using Microsoft.AspNetCore.Mvc;
using QLSinhVienCode.DTOs;
using QLSinhVienCode.Services;

namespace QLSinhVienCode.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var response = await _authService.LoginAsync(request);
            if (response == null)
            {
                return Unauthorized("Tên đăng nhập hoặc mật khẩu không đúng.");
            }
            return Ok(response);
        }
    }
}
