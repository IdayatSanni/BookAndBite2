using BookAndBite2.Models;

namespace BookAndBite2.Interfaces
{
    
        public interface IReviewService
        {
            // List all reviews with reviewer names and book information
            Task<IEnumerable<ReviewDto>> ListAllReviews();

           

        }
   }


