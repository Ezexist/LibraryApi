using Library.Data;
using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;
        public UserRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<User> CreateAsync(User user)
        {
            
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByUserNameAsync(string username)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserName == username);
            return user;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            var existUser = await _context.Users
                .AnyAsync(x => x.UserName == username);
            return existUser;
        }
    }
}
