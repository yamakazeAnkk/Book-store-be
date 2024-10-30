using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Google.Api.Gax;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly BookStoreContext _context;

        public OrderRepository(BookStoreContext bookStoreContext){
            _context = bookStoreContext;   
        }

        public async Task AddOrderAsync(Order order)
        {
            if (order.OrderDate == DateTime.MinValue)
            {
                order.OrderDate = DateTime.Now;  // Hoặc bạn có thể đặt một ngày cụ thể hợp lệ
            }
            order.Status = "Completed";
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> FindAllByUserAsync(int userId)
        {
            return await _context.Orders.Where(od => od.UserId == userId).ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders.Include( o=> o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<IEnumerable<Order>> GetRecentOrdersAsync(int page, int size)
        {
            return  await _context.Orders.OrderByDescending(o => o.OrderDate).Skip(page * size).Take(size).ToListAsync();
        }

        public async Task<double> GetTotalRevenueAsync()
        {
            return await _context.Orders.SumAsync(o => (double)o.TotalAmount);


        }
    }
}