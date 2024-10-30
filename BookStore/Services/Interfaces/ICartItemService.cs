using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Models;

namespace BookStore.Services.Interfaces
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItemDto>> GetCartItemsByUserIdAsync(int userId);
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);
        Task<CartItem> AddCartItemAsync(int productId,int quantity);
        Task UpdateCartItemAsync(int bookId,int quantity);
        Task DeleteCartItemAsync(int cartItemId);
        Task<IEnumerable<CartItem>> GetCartItemsAsync();
        Task ClearItemsAsync();

        Task ValidateCartItemsAsync();
    }
    
}