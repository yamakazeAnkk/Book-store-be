using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;

namespace BookStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        private readonly ICartItemRepository _cartItemRepository;

        private readonly IUserRepository _userRepository;

        private readonly IBookRepository _bookRepository;

        private readonly IOrderItemRepository _orderItemRepository;

        private readonly IVoucherRepository _voucherRepository;

        private readonly IVoucherUserRepository _voucherUserRepository;
        public OrderService(
            IOrderItemRepository orderItemRepository, 
            IMapper mapper, 
            IOrderRepository orderRepository,
            IBookRepository bookRepository,
            ICartItemRepository cartItemRepository,
            IUserRepository userRepository,
            IVoucherUserRepository voucherUserRepository,
            IVoucherRepository voucherRepository
            ){
            _orderRepository = orderRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _cartItemRepository = cartItemRepository;
            _bookRepository = bookRepository;
            _orderItemRepository = orderItemRepository;
            _voucherRepository = voucherRepository;
            _voucherUserRepository = voucherUserRepository;
        }
        public async Task CheckoutAsync(CreateOrderDto createOrderDto, string emailUser)
        {
            var currentUser = await _userRepository.GetUserByEmailAsync(emailUser)?? throw new Exception("User not found");
            var cartItems = await _cartItemRepository.GetCartItemsByUserIdAsync(currentUser.UserId);
            if(!cartItems.Any()){
                throw new Exception("Cart is empty");
            }
            var order = new Order{
                UserId = currentUser.UserId,
                Name = createOrderDto.Name,
                Phone = createOrderDto.Phone,
                Address = createOrderDto.Address,
                OrderItems = new List<OrderItem>()
            };
            decimal totalAmount = 0;
            foreach (var cartItem in cartItems)
            {
                var book = await _bookRepository.GetBookByIdAsync(cartItem.BookId);
                book.Quantity -= cartItem.Quantity;
                await _bookRepository.UpdateBookAsync(book);
                var orderItems = new OrderItem{
                    BookId = cartItem.BookId,
                    Quantity = cartItem.Quantity,
                    Order = order
                };
                order.OrderItems.Add(orderItems);
                totalAmount += book.Price * cartItem.Quantity;
            }
            
            var voucherUser = await _voucherUserRepository.GetUnusedVoucherByUserIdAsync(currentUser.UserId);
            if(voucherUser != null && voucherUser.IsUsed > 0){
                var voucher = await _voucherRepository.GetVoucherByIdAsync(voucherUser.VoucherId);
                if (voucher.ExpiredDate >= DateTime.UtcNow && totalAmount >= voucher.MinCost)
                {
                    decimal discount = Math.Min(voucher.Discount, totalAmount);
                    totalAmount -= discount;
                    
                    voucherUser.IsUsed -= 1;
                    await _voucherUserRepository.UpdateVoucherUserAsync(voucherUser);
                }
            }
            order.TotalAmount = totalAmount;
            await _orderRepository.AddOrderAsync(order);
            await _cartItemRepository.ClearCartItemsByUserIdAsync(currentUser.UserId);


        
        }

        public async Task<IEnumerable<OrderItemDto>> GetBestSellersAsync(int amount)
        {
            var bestSellers = await _orderItemRepository.GetBestSellersAsync();
            var bestSellerDto  =  bestSellers
            .OrderByDescending(pc => pc.Count)
            .Take(amount)
            .Select(pc => new OrderItemDto {
                BookId = (int)pc.BookId,
                Count = pc.Count,
                Quantity = pc.Count, 
                BookDto = _mapper.Map<BookDto>(pc.Book)
            }

            )
            .ToList();
            return bestSellerDto;
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetOrderByIdAsync(id);
        }

        public async Task<IEnumerable<OrderItemDto>> GetOrderDetailsAsync(long orderId)
        {
           var orderItems = await _orderItemRepository.FindAllByOrderIdAsync(orderId);
            return _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersRecentAsync(int pageNumber, int sizeNumber)
        {
            var orders = await _orderRepository.GetRecentOrdersAsync(pageNumber,sizeNumber);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<double> GetTotalRevenueAsync()
        {
            return await _orderRepository.GetTotalRevenueAsync();
        }
    }
}