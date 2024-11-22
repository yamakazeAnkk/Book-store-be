using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;

namespace BookStore.Services
{
    public class QuantityService : IQuantityService
    {
        private readonly IQuantityRepository _quantityRepository;

        public QuantityService(IQuantityRepository quantityRepository){
            _quantityRepository = quantityRepository;
        }
        public async Task<DashboardStatisticsDto> GetDashboardStatisticsAsync()
        {
            return await _quantityRepository.GetDashboardStatisticsAsync();
        }

        public async Task<IEnumerable<decimal>> GetMonthlyCompletedRevenueFor2024Async(int year)
        {
            try
            {
                return await _quantityRepository.GetTotalRevenueByYearAsync(year);
            }
            catch (ArgumentException ex)
            {
               
                throw new InvalidOperationException("An error occurred while fetching monthly revenue.", ex);
            }
        }

        public async Task<IEnumerable<decimal>> GetTotalRevenueByYearsAsync(int startYear, int endYear)
        {
            try
            {
                return await _quantityRepository.GetTotalRevenueByYearsAsync(startYear, endYear);
            }
            catch (ArgumentException ex)
            {
               
                throw new InvalidOperationException("An error occurred while fetching monthly revenue.", ex);
            }
        }
    }
}