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
    }
}