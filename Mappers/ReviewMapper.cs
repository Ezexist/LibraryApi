using Library.DTOs.Review;
using Library.Models;

namespace Library.Mappers
{
    public static class ReviewMapper
    {
        public static ReviewDto ToReviewDto(this Review reviewModel)
        {
            return new ReviewDto
            {
                Id = reviewModel.Id,
                Comment = reviewModel.Comment,
                Rating = reviewModel.Rating,
                BookId = reviewModel.BookId

            };
        }
        public static Review ToReviewFromDto(this CreateReviewDto createReviewDto)
        {
            return new Review
            {
                Comment = createReviewDto.Comment,
                Rating = createReviewDto.Rating,
                BookId = createReviewDto.BookId
            };
        }
    }
}
