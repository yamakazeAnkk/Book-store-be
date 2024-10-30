using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly BookStoreContext _bookStoreContext;
        public CartItemRepository(BookStoreContext bookStoreContext){
            _bookStoreContext = bookStoreContext;
        }
        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            await _bookStoreContext.CartItems.AddAsync(cartItem);
            await _bookStoreContext.SaveChangesAsync();
            return cartItem;
        }

        public async Task ClearCartItemsByUserIdAsync(int userId)
        {
            var cartItem = await _bookStoreContext.CartItems.Where(c => c.UserId == userId).ToListAsync();
            _bookStoreContext.RemoveRange(cartItem);
            await _bookStoreContext.SaveChangesAsync();
        }

        public async Task DeleteCartItemAsync(CartItem cartItem)
        {
            
            _bookStoreContext.CartItems.Remove(cartItem);
            await _bookStoreContext.SaveChangesAsync();
            
        }

        public async Task DeleteListCartItemAsync(IEnumerable<CartItem> cartItem)
        {
            _bookStoreContext.CartItems.RemoveRange(cartItem);
            await _bookStoreContext.SaveChangesAsync();
        }

        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await _bookStoreContext.CartItems.FindAsync(cartItemId);

        }

        public async Task<CartItem> GetCartItemByUserAndProductAsync(int userId, int productId)
        {
             return await _bookStoreContext.CartItems
            .FirstOrDefaultAsync(c => c.UserId == userId && c.BookId == productId);
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId)
        {
            return await _bookStoreContext.CartItems.Where(c => c.UserId == userId).Include(c => c.Book).ToListAsync();
        }

    

        public async Task SaveCartItemAsync(CartItem cartItem)
        {
            if (_bookStoreContext.CartItems.Any(c => c.CartItemId == cartItem.CartItemId))
            {
                _bookStoreContext.CartItems.Update(cartItem);
            }
            else
            {
                _bookStoreContext.CartItems.Add(cartItem);
            }
            await _bookStoreContext.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _bookStoreContext.CartItems.Update(cartItem);
            await _bookStoreContext.SaveChangesAsync();
        }
        
    }
}