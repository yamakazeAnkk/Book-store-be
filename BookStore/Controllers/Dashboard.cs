using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Dashboard : ControllerBase
    {
        private readonly IQuantityService _dashboardService;

        private readonly IOrderService _orderService;

        public Dashboard(IQuantityService dashboardService,IOrderService orderService)
        {
            _orderService = orderService;
            _dashboardService = dashboardService;
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetDashboardStatistics()
        {
            var stats = await _dashboardService.GetDashboardStatisticsAsync();
            return Ok(stats);
        }

        [HttpGet("revenue/quarter")]
        public async Task<IActionResult> GetRevenueByQuarter(int year ,int quarter){
            
            try
            {
                var total = await _orderService.GetTotalRevenueByQuarterAsync(year,quarter);
                return Ok(new {Year = year , Quarter = quarter , TotalRevenue = total});
            }
            catch (ArgumentException ex)
            {
                
                return BadRequest(new {message = ex.Message});
            }
        }
        [HttpGet("revenue/year")]
        public async Task<IActionResult> GetRevenueByYear(int year)
        {
            try
            {
                var totalRevenue = await _orderService.GetTotalRevenueByYearAsync(year);
                return Ok(new { Year = year, TotalRevenue = totalRevenue });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("monthly-revenue/{year}")]
        public async Task<IActionResult> GetMonthlyCompletedRevenue(int year)
        {
            try
            {
                var monthlyRevenue = await _dashboardService.GetMonthlyCompletedRevenueFor2024Async(year);
                return Ok(monthlyRevenue);
            }
            catch (InvalidOperationException ex)
            {
           
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
           
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }
        [HttpGet("revenue/years")]
        public async Task<IActionResult> GetRevenueByYears(int startYear, int endYear)
        {
            var revenue = await _dashboardService.GetTotalRevenueByYearsAsync(startYear, endYear);
            return Ok(revenue);
        }
    }
}