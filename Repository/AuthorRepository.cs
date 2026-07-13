using Library.Data;
using Library.DTOs.Author;
using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _context;
        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AuthorExistAsync(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }

        public async Task<Author> CreateAsync(Author authorModel)
        {
            await _context.Authors.AddAsync(authorModel);
            await _context.SaveChangesAsync();
            return authorModel;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors
                .AsNoTracking()
                .Include(a => a.Books)
                .ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors
                .AsNoTracking() 
                .Include(a => a.Books)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Author?> UpdateAsync(int id,UpdateAuthorDto updateAuthorDto)
        {
            var existingAuthor = await _context.Authors
                .FirstOrDefaultAsync(x => x.Id == id);
            if (existingAuthor == null)
                return null;
            existingAuthor.Name = updateAuthorDto.Name;
            existingAuthor.Country = updateAuthorDto.Country;
            await _context.SaveChangesAsync();
            return existingAuthor;
        }
        
    }
}
