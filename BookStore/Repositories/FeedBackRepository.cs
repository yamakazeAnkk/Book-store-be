using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private readonly BookStoreContext _context;
        public FeedBackRepository(BookStoreContext context){
            _context = context;
        }

        public async Task AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if(review != null){
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Review>> FindAllAsync()
        {
            return await _context.Reviews.OrderBy(f => f.ReviewId).ToListAsync();
        }

        public async Task<List<Review>> findAllByReview(Book book)
        {
            return await _context.Reviews.Include(r => r.User).Where(r => r.BookId == book.BookId).ToListAsync();
           
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<IEnumerable<Review>> GetReviewsByBookIdAsync(int id)
        {
            return await _context.Reviews.Where(u => u.BookId == id).ToListAsync();
        }

        public async Task<Review>SaveAsync(Review review)
        {
            if(review.ReviewId == 0){
                _context.Reviews.Add(review);
            }
            else
            {
                _context.Reviews.Update(review);
            }

            await _context.SaveChangesAsync();
            return review;
        }
    }
}