using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Models;

namespace BookStore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<double> GetTotalRevenueAsync();
        Task<IEnumerable<OrderDetailDto>> GetOrdersRecentAsync(int page, int size);
        Task<IEnumerable<OrderDetailDto>> GetOrdersRecentByUserAsync(string emailUser,int page, int size);
        Task<IEnumerable<OrderItemDto>> GetOrderDetailsAsync(long orderId);
        Task CheckoutAsync(CreateOrderDto createOrderDto, string userName);
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderItemDto>> GetBestSellersAsync(int amount);

        Task<IEnumerable<OrderDetailDto>> SearchOrdersByNameAsync(string name, int page, int size);

        Task<IEnumerable<OrderDetailDto>> SearchOrdersByDateAsync(int month, int year, int page, int size);
        
    }
}