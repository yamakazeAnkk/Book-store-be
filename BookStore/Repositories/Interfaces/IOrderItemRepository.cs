using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<long> GetAllSoldProductsAsync();
        Task<IEnumerable<OrderItem>> FindAllByOrderIdAsync(long orderId);
        Task<IEnumerable<ProductCount>> GetBestSellersAsync();
    }
}