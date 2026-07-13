using Library.DTOs.Book;
using Library.DTOs.Review;
using Library.Models;

namespace Library.Mappers
{
    public static class BookMapper
    {
        public static BookDto ToBookDto(this Book bookModel)
        {
            return new BookDto
            {
                Id = bookModel.Id,
                Title = bookModel.Title,
                Genre = bookModel.Genre,
                Pages = bookModel.Pages,
                AuthorName = bookModel.Author?.Name ?? string.Empty,
                ReviewCount = bookModel.Reviews?.Count ?? 0,
                AverageRating = bookModel.Reviews?.Any() == true ? bookModel.Reviews.Average(a => a.Rating) : 0,
                Reviews = bookModel.Reviews?
                .Select(b => b.ToReviewDto())
                .ToList() ?? new List<ReviewDto>()
            };
        }
        public static Book ToBookFromCreatedDto(this CreateBookDto createBookDto)
        {
            return new Book
            {
                Title = createBookDto.Title,
                Genre = createBookDto.Genre,
                Pages = createBookDto.Pages,
                AuthorId = createBookDto.AuthorId
            };
        } 
    }
}
