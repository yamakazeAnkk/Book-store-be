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
    }
}