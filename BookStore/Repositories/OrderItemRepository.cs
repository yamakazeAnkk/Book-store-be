using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class OrderItemRepository: IOrderItemRepository
    {
        private readonly BookStoreContext _context;
        public OrderItemRepository(BookStoreContext context){
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> FindAllByOrderIdAsync(long orderId)
        {
            return await _context.OrderItems.Where( x => x.OrderId == orderId).ToListAsync();
        }

        public async Task<long> GetAllSoldProductsAsync()
        {
            return await _context.OrderItems.SumAsync(od => od.Quantity);
        }

        public async Task<IEnumerable<ProductCount>> GetBestSellersAsync()
        {
            return await _context.OrderItems
                .Include(oi => oi.Book) 
                .GroupBy(od => od.BookId)
                .Select(g => new ProductCount
                {
                    Count = g.Sum(od => od.Quantity),
                    BookId = g.Key.Value,
                    Book = g.First().Book 
                })
                .OrderByDescending(pc => pc.Count)
                .ToListAsync();

             
        }

    }
}