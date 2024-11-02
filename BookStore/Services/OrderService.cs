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
            if (!string.IsNullOrWhiteSpace(createOrderDto.Code))
            {
                var voucher = await _voucherRepository.GetVoucherByCodeAsync(createOrderDto.Code);
                if (voucher != null && voucher.ExpiredDate >= DateTime.UtcNow && totalAmount >= voucher.MinCost)
                {
                    var voucherUser = await _voucherUserRepository.GetVoucherByUserIdAndVoucherIdAsync(currentUser.UserId, voucher.VoucherId);
                    if (voucherUser != null && voucherUser.IsUsed != 0)
                    {
                        decimal discount = Math.Min(voucher.Discount, totalAmount);
                        totalAmount -= discount;
                        voucherUser.IsUsed = 0; 
                        await _voucherUserRepository.UpdateVoucherUserAsync(voucherUser);
                    }
                    else
                    {
                        throw new Exception("Voucher is already used or invalid for this user.");
                    }
                }
                else
                {
                    throw new Exception("Invalid or expired voucher code.");
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
                
                Quantity = pc.Count, 
                BooksDto = _mapper.Map<BooksDto>(pc.Book)
            }

            )
            .ToList();
            return bestSellerDto;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order =  await _orderRepository.GetOrderByIdAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderItemDto>> GetOrderDetailsAsync(long orderId)
        {
           var orderItems = await _orderItemRepository.FindAllByOrderIdAsync(orderId);
            return _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);
        }

        public async Task<IEnumerable<OrderDetailDto>> GetOrdersRecentAsync(int pageNumber, int sizeNumber)
        {
            var orders = await _orderRepository.GetRecentOrdersAsync(pageNumber,sizeNumber);
            return _mapper.Map<IEnumerable<OrderDetailDto>>(orders);
        }


        public async Task<IEnumerable<OrderDetailDto>> GetOrdersRecentByUserAsync(string emailUser, int page, int size)
        {
            var user = await _userRepository.GetUserByEmailAsync(emailUser);
            if(user == null){
                throw new Exception("User not found");
            }
            var orders = await _orderRepository.GetRecentOrdersByUserIdAsync(user.UserId,page,size);
            return _mapper.Map<IEnumerable<OrderDetailDto>>(orders);
        }

        public async Task<double> GetTotalRevenueAsync()
        {
            return await _orderRepository.GetTotalRevenueAsync();
        }
        public async Task<IEnumerable<OrderDetailDto>> SearchOrdersByNameAsync(string name, int page, int size)
        {
            var orders = await _orderRepository.SearchOrdersByNameAsync(name, page, size);
            return _mapper.Map<IEnumerable<OrderDetailDto>>(orders);
        }

        public async Task<IEnumerable<OrderDetailDto>> SearchOrdersByDateAsync(int month, int year, int page, int size)
        {
            var orders = await _orderRepository.SearchOrdersByDateAsync(month, year, page, size);
            return _mapper.Map<IEnumerable<OrderDetailDto>>(orders);
        }

    }
}