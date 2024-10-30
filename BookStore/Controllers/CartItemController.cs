using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Services.Interfaces;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "user")]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        private readonly IUserService _userService;

        public CartItemController(ICartItemService cartItemService){
            _cartItemService = cartItemService;
        }
        [HttpPost]
        public async Task<IActionResult> AddItemCart(int productId , int quantity){
            try
            {
                await _cartItemService.AddCartItemAsync(productId,quantity);
                return Ok("item added to cart");
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCartItem([FromQuery]int productId, [FromQuery] int quantity)
        {
            if (quantity <= 0)
            {
                return BadRequest("Quantity must be greater than zero.");
            }

            try
            {
                await _cartItemService.UpdateCartItemAsync(productId, quantity);
                return Ok(new { message = "Cart item updated successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCartItems(){
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var cartItems = await _cartItemService.GetCartItemsByUserIdAsync(userId);

                return Ok(cartItems);
                

            }catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCartItem([FromQuery]int productId){
            try{
                await _cartItemService.DeleteCartItemAsync(productId);
                return Ok(new { message = "item deleted from cart"});
            }catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPost("clear")]
        public async Task<IActionResult> ClearCartItems(){
            try
            {
                await _cartItemService.ClearItemsAsync();
                return Ok(new { message = "All cart items cleared successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


    }
}