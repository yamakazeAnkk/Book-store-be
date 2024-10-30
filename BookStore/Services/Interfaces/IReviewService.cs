using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;

namespace BookStore.Services.Interfaces
{
    public interface IReviewService
    {
   
        Task<List<ReviewDto>> FindAllAsync();
        Task<ReviewDto> FindByIdAsync(int id);
        Task<long> CreateAsync(CreateReviewDto createReviewDto, string emailUser);
        Task UpdateAsync(int id, CreateReviewDto reviewDto);
        Task DeleteAsync(int id);
        Task<List<ReviewDetailDto>> FindAllByProductAsync(int productId);
    }
}