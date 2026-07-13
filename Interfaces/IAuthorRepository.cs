using Library.DTOs.Author;
using Library.Models;

namespace Library.Interfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author> CreateAsync(Author authorModel);
        Task<Author?> UpdateAsync(int id,UpdateAuthorDto updateAuthorDto);
        Task<bool> AuthorExistAsync(int id);
    }
}
