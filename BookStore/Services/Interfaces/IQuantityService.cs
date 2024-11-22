using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;

namespace BookStore.Services.Interfaces
{
    public interface IQuantityService
    {
        Task<DashboardStatisticsDto> GetDashboardStatisticsAsync();

        Task<IEnumerable<decimal>> GetMonthlyCompletedRevenueFor2024Async(int year);
        Task<IEnumerable<decimal>> GetTotalRevenueByYearsAsync(int startYear ,int endYear);
    }
}