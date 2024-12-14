using BookAndBite2.Data;
using BookAndBite2.Models;
using Microsoft.EntityFrameworkCore;
using BookAndBite2.Interfaces;


namespace BookAndBite2.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;

    // Dependency injection of database context
    public ReviewService(ApplicationDbContext context)
    {
        _context = context;
    }


        // List all reviews with reviewer names and book information
        public async Task<IEnumerable<ReviewDto>> ListAllReviews()
        {
            return await _context.Reviews
                .Include(r => r.CartId) // Include the Cart
                //.Include(r => r.Book)     // Include the Book
                .Select(r => new ReviewDto
                {
                    ReviewId = r.ReviewId,
                    ReviewText = r.ReviewText,
                    ReviewDate = r.ReviewDate,
                    
                })
                .ToListAsync();
        }
    }
}
