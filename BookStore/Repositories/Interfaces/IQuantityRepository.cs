using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;

namespace BookStore.Repositories.Interfaces
{
    public interface IQuantityRepository
    {
        Task<DashboardStatisticsDto> GetDashboardStatisticsAsync();
    }
}