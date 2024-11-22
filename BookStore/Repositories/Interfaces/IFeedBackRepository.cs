using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface IFeedBackRepository
    {
        Task<List<Review>>findAllByReview(Book book);

        Task<Review> GetReviewByIdAsync(int id);
        
        Task<IEnumerable<Review>> GetReviewsByBookIdAsync(int id);
        
        Task<Review> SaveAsync (Review review);

        Task DeleteReviewAsync (int id);
        Task AddReviewAsync(Review review);
        
        Task<List<Review>> FindAllAsync();
        


    }
}