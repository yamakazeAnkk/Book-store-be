using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface ICartItemRepository
    {
        
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task DeleteListCartItemAsync(IEnumerable<CartItem> cartItem);
        Task DeleteCartItemAsync(CartItem cartItem);

        Task<CartItem> GetCartItemByUserAndProductAsync(int userId, int productId);
        Task SaveCartItemAsync(CartItem cartItem);

        
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId);
        Task ClearCartItemsByUserIdAsync(int userId);
    }
}