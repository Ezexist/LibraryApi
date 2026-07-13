using Library.DTOs.Book;
using Library.DTOs.Other;
using Library.Models;
using Library.QueryObjects;

namespace Library.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync(BookQueryObject bookQueryObject);
        Task<Book?> GetByIdAsync (int id);
        Task<Book> CreateAsync(Book book);
        Task<Book?> UpdateAsync(int id, UpdateBookDto updateBookDto);
        Task<List<Book>> GetTopRatedAsync(float rating);

        Task<List<Book>> GetByAuthorAsync(int AuthotId);
        Task<List<Book>> GetMostReviewedAsync(int count);
        Task<List<GenreStatisticsDto>> GetGenreStatistic();
    }
}
