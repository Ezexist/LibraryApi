using Library.Models;

namespace Library.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string username);

        Task<User?> GetByUserNameAsync(string username);

        Task<User> CreateAsync(User user);
    }
}
