using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Helper;
using BookStore.Models;
using Google.Api.Gax;

namespace BookStore.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<double> GetTotalRevenueAsync();
        Task<PaginatedResult<Order>> FindAllByUserAsync(int userId,int page ,int size);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<PaginatedResult<Order>> GetRecentOrdersAsync(int page, int size);
        Task AddOrderAsync(Order order);

        Task UpdateOrderAsync(Order order);

        Task<decimal>GetTotalRevenueByQuarterAsync(int year ,int quarter);

        Task<decimal> GetTotalRevenueByYearAsync(int year);

        Task DeleteOrderByIdAsync(int id );
        Task <PaginatedResult<Order>>FilterOrderByStateAsync(string state, int page ,int size );

        Task<PaginatedResult<Order>> SearchOrdersByNameAsync(string name,int page, int size);
        Task<PaginatedResult<Order>> SearchOrdersByDateAsync(int month, int year,int page, int size);

        Task<PaginatedResult<Order>> GetRecentOrdersByUserIdAsync(int userId, int page, int size);

        Task<PaginatedResult<Order>> SearchAllOrderAsync(string name,string month, string state ,int page ,int size);
        
    }
}