using Library.DTOs.User;
using Library.Interfaces;
using Library.Middleware.Exeption;
using Library.Models;

namespace Library.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<string> LogingAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUserNameAsync(loginDto.UserName);
            if (user == null)
                throw new BadRequestException("Invalid username or password");

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(
                loginDto.Password,
                user.PasswordHash);

            if (!isPasswordValid)
                throw new BadRequestException("Invalid username or password");

            var token = _tokenService.CreateToken(user);

            return token;
        }

        public async Task<User> RegisterAsync(RegisterDto registerDto)
        {
            if (await _userRepository.UserExistsAsync(registerDto.UserName))
                throw new BadRequestException("Username already exists");
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                Role = "User"
            };
            await _userRepository.CreateAsync(user);
            return user;
        }


    }
}
