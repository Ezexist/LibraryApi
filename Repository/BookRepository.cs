using Library.Data;
using Library.DTOs.Book;
using Library.DTOs.Other;
using Library.Interfaces;
using Library.Mappers;
using Library.Middleware.Exeption;
using Library.Models;
using Library.QueryObjects;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<Book> CreateAsync(Book book)
        {
            var authorExist = await _context.Authors.AnyAsync(a => a.Id == book.AuthorId);
            if (!authorExist) throw new NotFoundException("Author not found");

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<List<Book>> GetAllAsync(BookQueryObject queryObject)
        {
            var books = _context.Books
                .AsNoTracking()
                .Include(b => b.Reviews)
                .Include(b => b.Author)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(queryObject.Title))
            {
                books = books.Where(b => b.Title.Contains(queryObject.Title));
            }
            if (!string.IsNullOrWhiteSpace(queryObject.Genre))
            {
                books = books.Where(b => b.Genre.Contains(queryObject.Genre));
            }
            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if(queryObject.SortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    books = queryObject.IsDescending ? books.OrderByDescending(x => x.Title)
                        : books.OrderBy(x => x.Title);
                }
                else if (queryObject.SortBy.Equals("Genre", StringComparison.OrdinalIgnoreCase))
                {
                    books = queryObject.IsDescending ? books.OrderByDescending(x => x.Genre)
                        : books.OrderBy(x => x.Genre);
                }
                else if (queryObject.SortBy.Equals("Pages", StringComparison.OrdinalIgnoreCase))
                {
                    books = queryObject.IsDescending
                        ? books.OrderByDescending(x => x.Pages)
                        : books.OrderBy(x => x.Pages);
                }
            }
            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
            return await books.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<List<Book>> GetByAuthorAsync(int AuthotId)
        {
            var books = await _context.Books.Where(b => b.AuthorId == AuthotId)
                .Include(b => b.Author)
                .Include(b => b.Reviews)
                .AsNoTracking()
                .ToListAsync();
            
            return books;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            var book = await _context.Books
                .AsNoTracking() 
                .Include(b => b.Reviews)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
                throw new NotFoundException("Book not found");
            return book;
        }

        public async Task<List<GenreStatisticsDto>> GetGenreStatistic()
        {
            var statistic = await _context.Books.GroupBy(b => b.Genre).Select(group =>
            new GenreStatisticsDto
            {
                BookCount = group.Count(),
                Genre = group.Key,
                AveragePages = group.Average(b => b.Pages)
                
            }).ToListAsync();
            return statistic;
        }

        public async Task<List<Book>> GetMostReviewedAsync(int count)
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Reviews) .AsNoTracking()
                .OrderByDescending(b => b.Reviews.Count).Take(count).ToListAsync();
            return books;
        }

        public async Task<List<Book>> GetTopRatedAsync(float rating)
        {
            var books =  await _context.Books.Include(b => b.Author).Include(b => b.Reviews)
                .AsNoTracking()
                .Where(b => b.Reviews.Any() && b.Reviews.Average(r => r.Rating) >= rating)
                .ToListAsync();
            return books;
        }

        public async Task<Book?> UpdateAsync(int id, UpdateBookDto updateBookDto)
        {
            var existingBook = await _context.Books
                .FirstOrDefaultAsync(x => x.Id == id);
            if (existingBook == null)
                throw new NotFoundException("Book not found");
            existingBook.Title = updateBookDto.Title;
            existingBook.Genre = updateBookDto.Genre;
            existingBook.Pages = updateBookDto.Pages;
            existingBook.AuthorId = updateBookDto.AuthorId;
            await _context.SaveChangesAsync();
            return existingBook;
        }
    }
}
