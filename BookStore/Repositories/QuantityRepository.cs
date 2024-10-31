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
    public class QuantityRepository : IQuantityRepository
    {
        private readonly BookStoreContext _bookStoreContext;

        public QuantityRepository(BookStoreContext bookStoreContext){
            _bookStoreContext = bookStoreContext;
        }
        public async Task<DashboardStatisticsDto> GetDashboardStatisticsAsync()
        {
            var bookCount = await _bookStoreContext.Books.CountAsync();
            var orderCount = await _bookStoreContext.Orders.CountAsync();
            var userCount = await _bookStoreContext.Users.CountAsync();
            var totalOrderAmount = await _bookStoreContext.Orders.SumAsync(o => o.TotalAmount);

            return new DashboardStatisticsDto
            {
                BookCount = bookCount,
                OrderCount = orderCount,
                UserCount = userCount,
                TotalOrderAmount = totalOrderAmount
            };
        }
    }
}