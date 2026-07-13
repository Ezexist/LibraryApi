using Library.DTOs.User;
using Library.Interfaces;
using Library.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public AuthController(IUserRepository userRepository
            , IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var user = await _authService.RegisterAsync(registerDto);
            return Ok(user.ToUserDto());
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            var token = await _authService.LogingAsync(loginDto);
            return Ok(new
            {
                token
            });
        }
    }
}
