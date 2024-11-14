using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Helper;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BookStoreContext _context;

        public OrderRepository(BookStoreContext bookStoreContext)
        {
            _context = bookStoreContext;
        }

        public async Task AddOrderAsync(Order order)
        {
            if (order.OrderDate == DateTime.MinValue)
            {
                order.OrderDate = DateTime.Now;
            }
            order.Status = "Processing";
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderByIdAsync(int id)
        {
            var order = await GetOrderByIdAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedResult<Order>> FilterOrderByStateAsync(string state, int page, int size)
        {
            var query = _context.Orders
                .Where(o => o.Status.ToLower() == state.ToLower())
                .Include(o => o.OrderItems);

            int totalCount = await query.CountAsync();
            var orders = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginatedResult<Order>(orders, totalCount, size);
        }

        public async Task<PaginatedResult<Order>> FindAllByUserAsync(int userId, int page, int size)
        {
            var query = _context.Orders.Where(o => o.UserId == userId);

            int totalCount = await query.CountAsync();
            var orders = await query
                .Include(o => o.OrderItems)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginatedResult<Order>(orders, totalCount, size);
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ThenInclude(b => b.BookBrands)
                .ThenInclude(bb => bb.Band)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<PaginatedResult<Order>> GetRecentOrdersAsync(int page, int size)
        {
            var query = _context.Orders.OrderByDescending(o => o.OrderDate);

            int totalCount = await query.CountAsync();
            var orders = await query
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ThenInclude(b => b.BookBrands)
                .ThenInclude(bb => bb.Band)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginatedResult<Order>(orders, totalCount, size);
        }

        public async Task<PaginatedResult<Order>> GetRecentOrdersByUserIdAsync(int userId, int page, int size)
        {
            var query = _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate);

            int totalCount = await query.CountAsync();
            var orders = await query
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .ThenInclude(b => b.BookBrands)
                .ThenInclude(bb => bb.Band)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginatedResult<Order>(orders, totalCount, size);
        }

        public async Task<double> GetTotalRevenueAsync()
        {
            return await _context.Orders.SumAsync(o => (double)o.TotalAmount);
        }

        public async Task<decimal> GetTotalRevenueByQuarterAsync(int year, int quarter)
        {
            if (quarter < 1 || quarter > 4)
            {
                throw new ArgumentException("Invalid quarter");
            }

            int startMonth = (quarter - 1) * 3 + 1;
            int endMonth = quarter * 3;

            return await _context.Orders
                .Where(o => o.Status == "Completed" && o.OrderDate.Year == year && o.OrderDate.Month >= startMonth && o.OrderDate.Month <= endMonth)
                .SumAsync(o => o.TotalAmount);
        }

        public async Task<decimal> GetTotalRevenueByYearAsync(int year)
        {
            return await _context.Orders
                .Where(o => o.Status == "Completed" && o.OrderDate.Year == year)
                .SumAsync(o => o.TotalAmount);
        }

        public async Task<PaginatedResult<Order>> SearchAllOrderAsync(FilterOrderDto filterOrderDto,int page, int size)
        {
            var query = _context.Orders.AsQueryable();
            if (!string.IsNullOrEmpty(filterOrderDto.Status))
            {
                query = query.Where(b => b.Status.ToLower() == filterOrderDto.Status.ToLower());
            }

            if (filterOrderDto.Month.HasValue)
            {
                query = query.Where(b => b.OrderDate.Month == filterOrderDto.Month.Value);
            }

            if (filterOrderDto.Year.HasValue)
            {
                query = query.Where(b => b.OrderDate.Year == filterOrderDto.Year.Value);
            }

            if (!string.IsNullOrEmpty(filterOrderDto.Phone))
            {
                query = query.Where(b => b.Phone == filterOrderDto.Phone);
            }
            int totalCount = await query.CountAsync();
            
            var order = await query.Include(o => o.OrderItems).Skip((page - 1) *size).Take(size).ToListAsync();

            return new PaginatedResult<Order>(order,totalCount,size);
            
        }

        public async Task<PaginatedResult<Order>> SearchOrdersByDateAsync(int month, int year, int page, int size)
        {
            var query = _context.Orders
                .Where(o => o.OrderDate.Month == month && o.OrderDate.Year == year);

            int totalCount = await query.CountAsync();
            var orders = await query
                .Include(o => o.OrderItems)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginatedResult<Order>(orders, totalCount, size);
        }

        public async Task<PaginatedResult<Order>> SearchOrdersByNameAsync(string name, int page, int size)
        {
            var query = _context.Orders
            .Include(o => o.OrderItems) // Đưa `Include` lên trước để đảm bảo dữ liệu được tải trước khi phân trang
            .Where(o => EF.Functions.Like(o.Name.ToLower(), $"%{name.ToLower()}%")); // Sử dụng `EF.Functions.Like` để tìm kiếm theo tên

            int totalCount = await query.CountAsync();
            var orders = await query
                .Include(o => o.OrderItems)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginatedResult<Order>(orders, totalCount, size);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
