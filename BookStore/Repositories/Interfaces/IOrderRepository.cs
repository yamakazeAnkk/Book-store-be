using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<double> GetTotalRevenueAsync();
        Task<IEnumerable<Order>> FindAllByUserAsync(int userId);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetRecentOrdersAsync(int page, int size);
        Task AddOrderAsync(Order order);
    }
}