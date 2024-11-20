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
            var totalOrderAmount = await _bookStoreContext.Orders.Where(u => u.Status.ToUpper() == "Completed").SumAsync(o => o.TotalAmount);
            var reviewCount = await _bookStoreContext.Reviews.CountAsync();
            return new DashboardStatisticsDto
            {
                BookCount = bookCount,
                OrderCount = orderCount,
                UserCount = userCount,
                TotalOrderAmount = totalOrderAmount,
                ReviewCount = reviewCount
            };
        }

        public async Task<IEnumerable<decimal>> GetTotalRevenueByYearAsync(int year)
        {
           
            var months = new List<decimal>();
            for(int i = 1; i <= 12; i++){
                var totalForMonth = await _bookStoreContext.Orders
                .Where(o => o.Status == "Completed" && o.OrderDate.Year == year && o.OrderDate.Month == i)
                .SumAsync(o => o.TotalAmount);
                months.Add(totalForMonth);
            }
            return months.AsEnumerable();
        }

        public async Task<IEnumerable<decimal>> GetTotalRevenueByYearsAsync(int startYear, int endYear)
        {
            var years = new List<decimal>();
            for(int i = startYear; i <= endYear; i++){
                var totalForYear = await _bookStoreContext.Orders
                .Where(o => o.Status == "Completed" && o.OrderDate.Year == i)
                .SumAsync(o => o.TotalAmount);
                years.Add(totalForYear);
             
            }
            return years.AsEnumerable();
        }
    }
}