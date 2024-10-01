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
        Task<IEnumerable<OrderDto>> GetOrdersRecentAsync(int page, int size);
        Task<IEnumerable<OrderItemDto>> GetOrderDetailsAsync(long orderId);
        Task CheckoutAsync(CreateOrderDto createOrderDto, string userName);
        Task<Order?> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderItemDto>> GetBestSellersAsync(int amount);

        
    }
}