using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Helper;
using BookStore.Models;
using Google.Api.Gax;

namespace BookStore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<double> GetTotalRevenueAsync();
        Task<PaginatedResult<OrderDetailDto>> GetOrdersRecentAsync(int page, int size);
        Task<PaginatedResult<OrderDetailDto>> GetOrdersRecentByUserAsync(string emailUser,int page, int size);
        Task<IEnumerable<OrderItemDto>> GetOrderDetailsAsync(long orderId);
        Task CheckoutAsync(CreateOrderDto createOrderDto, string userName);

        Task UpdateStateAsync(int orderId , string state);

        Task CancelOrderAsync(int orderId , string userName);

        Task UpdateOrderInformationAsync(int orderId, UpdateOrderInformationDto updateOrderInformationDto);
        Task<decimal> GetTotalRevenueByQuarterAsync(int year, int quarter);
        Task<decimal> GetTotalRevenueByYearAsync(int year);

        Task<PaginatedResult<OrderDetailDto>> FilterOrderByStateAsync(string status,int page,int size );
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderItemDto>> GetBestSellersAsync(int amount);

        Task<PaginatedResult<OrderDetailDto>> SearchOrdersByNameAsync(string name, int page, int size);

        Task<PaginatedResult<OrderDetailDto>> SearchOrdersByDateAsync(int month, int year, int page, int size);

        Task<PaginatedResult<OrderDetailDto>> SearchOrderAllAsync(FilterOrderDto filterOrderDto, int page, int size);
        
    }
}