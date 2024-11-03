using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Helper;
using BookStore.Models;
using Google.Api.Gax;

namespace BookStore.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<long> GetAllSoldProductsAsync();
        Task<IEnumerable<OrderItem>> FindAllByOrderIdAsync(long orderId);
        Task<IEnumerable<ProductCount>> GetBestSellersAsync();
    }
}