using Library.DTOs.User;
using Library.Models;

namespace Library.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterDto registerDto);
        Task<string> LogingAsync(LoginDto loginDto);
    }
}
