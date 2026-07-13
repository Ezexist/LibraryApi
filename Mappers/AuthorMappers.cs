using Library.DTOs.Author;
using Library.Models;

namespace Library.Mappers
{
    public static class AuthorMappers
    {
        public static AuthorDto ToAuthorDto(this Author authorModel)
        {
            return new AuthorDto
            {
                Id = authorModel.Id,
                Name = authorModel.Name,
                Country = authorModel.Country,
                Books = authorModel.Books
                .Select(b => b.Title)
                .ToList()
                
            };
        }
        public static Author ToAuthorFromCreateDto(this CreateAuthorDto createAuthorDto)
        {
            return new Author
            {
                Name = createAuthorDto.Name,
                Country = createAuthorDto.Country
            };
        }
    }
}
