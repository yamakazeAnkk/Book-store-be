using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IMapper _mapper;
        

        private readonly IBookRepository _bookRepository;
        public CartItemService(ICartItemRepository cartItemRepository,IMapper mapper, IHttpContextAccessor httpContextAccessor,IBookRepository bookRepository){
            _cartItemRepository = cartItemRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            
        }
        public async Task<CartItem> AddCartItemAsync(int productId,int quantity)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }
            var product = await _bookRepository.GetBookByIdAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            var cartItem = await _cartItemRepository.GetCartItemByUserAndProductAsync(int.Parse(userId), productId);

            if (cartItem != null)
            {
                
                cartItem.Quantity += quantity;
            }
            else
            {
                
                cartItem = new CartItem
                {
                    UserId = int.Parse(userId),
                    BookId = productId,
                    Quantity = quantity
                };
            }
            await _cartItemRepository.SaveCartItemAsync(cartItem);
            return cartItem;
        }

        public async Task DeleteCartItemAsync(int cartItemId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null){
                throw new UnauthorizedAccessException("user not authenticated");

            }
            var cartItem = await _cartItemRepository.GetCartItemByUserAndProductAsync(int.Parse(userId),cartItemId);
            if (cartItem != null){
                await _cartItemRepository.DeleteCartItemAsync(cartItem);
            }

        }

        public Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync()
        {
            var userId= _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId == null){
                throw new UnauthorizedAccessException("User not authenticated");
            }
            return await _cartItemRepository.GetCartItemsByUserIdAsync(int.Parse(userId));
        }

        public async Task<IEnumerable<CartItemDto>> GetCartItemsByUserIdAsync(int userId)
        {
            var cartItems = await _cartItemRepository.GetCartItemsByUserIdAsync(userId);

            var cartItemDto = cartItems.Select(cartItem => MaptoDTO(cartItem)).ToList();

            return cartItemDto;
        }

        
        public async Task ValidateCartItemsAsync()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }

            var cartItems = await _cartItemRepository.GetCartItemsByUserIdAsync(int.Parse(userId));

            foreach (var cartItem in cartItems)
            {
                var product = await _bookRepository.GetBookByIdAsync(cartItem.BookId);
                if (cartItem.Quantity > product.Quantity)
                {
                    throw new Exception($"Product {product.Title} only has {product.Quantity} items in stock.");
                }
            }
        }
        public async Task ClearItemsAsync()
        {
           
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }

          
            var cartItems = await _cartItemRepository.GetCartItemsByUserIdAsync(int.Parse(userId));

            if (cartItems == null || !cartItems.Any())
            {
                return; 
            }

           
            await _cartItemRepository.DeleteListCartItemAsync(cartItems);
        }

        public async Task UpdateCartItemAsync(int bookId, int quantity)
        {
           var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
           if(userId == null){
                throw new UnauthorizedAccessException("User not authenticated");
           }
           var cartItem = await _cartItemRepository.GetCartItemByUserAndProductAsync(int.Parse(userId),bookId);
           if(cartItem == null){
                throw new Exception("Product not found in cart");
           }
           cartItem.Quantity = quantity;

           await _cartItemRepository.SaveCartItemAsync(cartItem);
           

        }

        private CartItemDto MaptoDTO(CartItem cartItem){
            var cartItemDto = new CartItemDto{
                quantity = cartItem.Quantity,
                total = cartItem.Quantity * cartItem.Book.Price,
                bookDto = _mapper.Map<BookDto>(cartItem.Book)

            };
            return cartItemDto;
        }
    }
}