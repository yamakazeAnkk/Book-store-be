using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        
        public OrdersController(IOrderService orderService){
            _orderService = orderService;
            
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentOrders(int page = 1, int size = 10)
        {
            
            var orders = await _orderService.GetOrdersRecentAsync(page, size);
            return Ok(orders);
        }
        [Authorize(Roles = "user")]
        [HttpGet("user")]
        public async Task<IActionResult> GetRecentOrdersByUser(int page = 1, int size = 10){
            var emailUser = User.FindFirst(ClaimTypes.Email)?.Value;
                if (string.IsNullOrEmpty(emailUser))
                    return Unauthorized("User not authenticated");
            var orders = await _orderService.GetOrdersRecentByUserAsync(emailUser, page, size);
            if (orders == null || !orders.Items.Any())
                return NotFound(new { message = "No recent orders found for this user" });

            return Ok(orders);
        }

        [HttpGet("best-sellers")]
        public async Task<IActionResult> GetBestSellers(int amount = 10)
        {
            var bestSellers = await _orderService.GetBestSellersAsync(amount);
            return Ok(bestSellers);
        }
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus(int id,string status){
            try
            {
             
                await _orderService.UpdateStateAsync(id,status);

                return Ok(new { message = "Status update successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [Authorize(Roles = "user")]
        [HttpPut("cancel-status")]
        public async Task<IActionResult> CancelOrder(int id){
            try
            {
                var emailUser = User.FindFirst(ClaimTypes.Email)?.Value;
                if (string.IsNullOrEmpty(emailUser))
                    return Unauthorized("User not authenticated");
                await _orderService.CancelOrderAsync(id,emailUser);

                return Ok(new { message = "Status update successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
         [HttpGet("search")]
        public async Task<IActionResult> SearchOrderAll(string? phone , string? status, int? year, int? month, int page = 1 , int size = 10){
            var filterOrderDto = new FilterOrderDto{
                Phone = phone,
                Status = status,
                Year = year,
                Month = month,
            };
            var order = await _orderService.SearchOrderAllAsync(filterOrderDto,page,size);
            return Ok(order);
        }
        
        
        [Authorize(Roles = "user")]
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CreateOrderDto createOrderDto)
        {
            try
            {
                
                var emailUser = User.FindFirst(ClaimTypes.Email)?.Value;
                if (string.IsNullOrEmpty(emailUser))
                    return Unauthorized("User not authenticated");

            
                await _orderService.CheckoutAsync(createOrderDto, emailUser);

                return Ok(new { message = "Order placed successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }
        [HttpGet("search-by-name")]
        public async Task<IActionResult> SearchOrdersByName(string name, int page = 1, int size = 10)
        {
            var orders = await _orderService.SearchOrdersByNameAsync(name, page, size);
            return Ok(orders);
        }

        
        
        [HttpGet("search-by-date")]
        public async Task<IActionResult> SearchOrdersByDate(int month, int year, int page = 1, int size = 10)
        {
            var orders = await _orderService.SearchOrdersByDateAsync(month, year, page, size);
            return Ok(orders);
        }
        [HttpGet("filter-status")]
        public async Task<IActionResult> FilterStatus(string status,int page = 1, int size = 10){
            var order = await _orderService.FilterOrderByStateAsync(status,page,size);
            return Ok(order);
        }
    }
}