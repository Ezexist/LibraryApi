using Library.Data;
using Library.DTOs.Review;
using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly LibraryDbContext _context;
        public ReviewRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<Review?> CreateAsync(Review review)
        {
            var bookExist = await _context.Books.AnyAsync(b => b.Id == review.BookId);
            if (!bookExist)
                return null;
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews
                .AsNoTracking()
                .FirstOrDefaultAsync(review => review.Id == id);
        }

        public async Task<Review?> UpdateAsync(int id, UpdateReviewDto updateDto)
        {
            var existingReview = await _context.Reviews.FirstOrDefaultAsync(review => review.Id == id);
        if(existingReview == null) 
                return null;

            var bookExist = await _context.Books.AnyAsync(b => b.Id == updateDto.BookId);
            if (!bookExist)
                return null;
            existingReview.Rating = updateDto.Rating;
            existingReview.Comment = updateDto.Comment;
            existingReview.BookId = updateDto.BookId;
            await _context.SaveChangesAsync();
            return existingReview;
        }
    }
}
