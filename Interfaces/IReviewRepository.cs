using Library.DTOs.Review;
using Library.Models;

namespace Library.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(int id);
        Task<Review?> CreateAsync(Review review);
        Task<Review?> UpdateAsync(int id, UpdateReviewDto updateReviewDto);
    }
}
