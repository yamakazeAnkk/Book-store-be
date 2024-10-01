using System;
using System.Collections.Generic;
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
    [Authorize(Roles = "user")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService){
            _orderService = orderService;
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentOrders(int page = 0, int size = 5)
        {
            var orders = await _orderService.GetOrdersRecentAsync(page, size);
            return Ok(orders);
        }

        [HttpGet("best-sellers")]
        public async Task<IActionResult> GetBestSellers([FromQuery] int amount = 0)
        {
            var bestSellers = await _orderService.GetBestSellersAsync(amount);
            return Ok(bestSellers);
        }

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
    }
}