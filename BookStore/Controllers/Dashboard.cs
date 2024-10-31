using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Dashboard : ControllerBase
    {
        private readonly IQuantityService _dashboardService;

        public Dashboard(IQuantityService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetDashboardStatistics()
        {
            var stats = await _dashboardService.GetDashboardStatisticsAsync();
            return Ok(stats);
        }
    }
}