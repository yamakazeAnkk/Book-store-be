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
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetRecentOrdersAsync(int page, int size);
        Task AddOrderAsync(Order order);

        Task<IEnumerable<Order>> SearchOrdersByNameAsync(string name,int page, int size);
        Task<IEnumerable<Order>> SearchOrdersByDateAsync(int month, int year,int page, int size);

        Task<IEnumerable<Order>> GetRecentOrdersByUserIdAsync(int userId, int page, int size);
    }
}