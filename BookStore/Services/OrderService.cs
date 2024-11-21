using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.DTOs;
using BookStore.Helper;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using Microsoft.VisualBasic;

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

        private readonly IPurchasedEbookRepository _purchasedEbookRepository;

        private readonly IEmailService _emailService;
        private readonly IVoucherUserRepository _voucherUserRepository;
        public OrderService(
            IOrderItemRepository orderItemRepository, 
            IMapper mapper, 
            IOrderRepository orderRepository,
            IBookRepository bookRepository,
            ICartItemRepository cartItemRepository,
            IUserRepository userRepository,
            IVoucherUserRepository voucherUserRepository,
            IVoucherRepository voucherRepository,
            IPurchasedEbookRepository purchasedEbookRepository,
            IEmailService emailService
            ){
            _orderRepository = orderRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _cartItemRepository = cartItemRepository;
            _bookRepository = bookRepository;
            _orderItemRepository = orderItemRepository;
            _voucherRepository = voucherRepository;
            _voucherUserRepository = voucherUserRepository;
            _purchasedEbookRepository = purchasedEbookRepository;
            _emailService = emailService;
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
            bool allEbooks = true;
            foreach (var cartItem in cartItems)
            {
                var book = await _bookRepository.GetBookByIdAsync(cartItem.BookId);

                
                if (book.TypeBookId == 1)
                {
                    // Nếu loại sách là 1, trừ Quantity
                    if(book.Quantity == 0 || book.Quantity < cartItem.Quantity){
                        throw new Exception($"Not enough stock for the book '{book.Title}'. Requested: {cartItem.Quantity}, Available: {book.Quantity}");
                    }
                    book.Quantity -= cartItem.Quantity;
                    await _bookRepository.UpdateBookAsync(book);
                    allEbooks = false;
                }
                else if (book.TypeBookId == 2)
                {
                    // Nếu loại sách là 2, lưu vào bảng PurchasedEbook mà không trừ Quantity
                    var purchasedEbook = new PurchasedEbook
                    {
                        UserId = currentUser.UserId,
                        BookId = book.BookId,
                        PurchaseDate = DateTime.UtcNow
                    };
                    await _purchasedEbookRepository.AddPurchasedEbookAsync(purchasedEbook);
                }
                
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
                    var voucherUser = await _voucherUserRepository.GetVoucherByUserAndVoucherIdAsync(currentUser.UserId, voucher.VoucherId);
                    if (voucherUser != null && voucherUser.IsUsed == 0)
                    {
                        decimal discount = Math.Min(voucher.Discount, totalAmount);
                        totalAmount *= (1 - discount);
                        voucherUser.IsUsed = 1; 
                        await _voucherUserRepository.UpdateVoucherUserAsync(voucherUser);
                    }
                    
                }
               
            }
            order.Status = allEbooks ? "Completed" : "Pending";
            order.TotalAmount = totalAmount;
            await _orderRepository.AddOrderAsync(order);
            await _cartItemRepository.ClearCartItemsByUserIdAsync(currentUser.UserId);
            var emailBody = $@"
            Dear {currentUser.Fullname},

            Thank you for your order! Here are the details:
            
            - Total Amount: {order.TotalAmount:000} VNĐ
            - Status: {order.Status}

            Your order will be processed soon. If you have any questions, feel free to contact us.

            Best regards,
            BookStore Team";

        await _emailService.SendEmailAsync(currentUser.Email, "Order Confirmation", emailBody);


        
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

        public async Task<PaginatedResult<OrderDetailDto>> GetOrdersRecentAsync(int pageNumber, int sizeNumber)
        {
            var orders = await _orderRepository.GetRecentOrdersAsync(pageNumber,sizeNumber);
            var orderDtos = _mapper.Map<IEnumerable<OrderDetailDto>>(orders.Items);

   
            return new PaginatedResult<OrderDetailDto>(orderDtos, orders.TotalCount, sizeNumber);
        }


        public async Task<PaginatedResult<OrderDetailDto>> GetOrdersRecentByUserAsync(string emailUser, int page, int size)
        {
            var user = await _userRepository.GetUserByEmailAsync(emailUser);
            if(user == null){
                throw new Exception("User not found");
            }
            var orders = await _orderRepository.GetRecentOrdersByUserIdAsync(user.UserId, page, size);
            var orderDtos = _mapper.Map<IEnumerable<OrderDetailDto>>(orders.Items);

 
            return new PaginatedResult<OrderDetailDto>(orderDtos, orders.TotalCount, size);
        }

        public async Task<double> GetTotalRevenueAsync()
        {
            return await _orderRepository.GetTotalRevenueAsync();
        }
        public async Task<PaginatedResult<OrderDetailDto>> SearchOrdersByNameAsync(string name, int page, int size)
        {
            var orders = await _orderRepository.SearchOrdersByNameAsync(name, page, size);
            var orderDtos = _mapper.Map<IEnumerable<OrderDetailDto>>(orders.Items);

   
            return new PaginatedResult<OrderDetailDto>(orderDtos, orders.TotalCount, size);
        }

        public async Task<PaginatedResult<OrderDetailDto>> SearchOrdersByDateAsync(int month, int year, int page, int size)
        {
            var orders = await _orderRepository.SearchOrdersByDateAsync(month, year, page, size);
            var orderDtos = _mapper.Map<IEnumerable<OrderDetailDto>>(orders.Items);

   
            return new PaginatedResult<OrderDetailDto>(orderDtos, orders.TotalCount, size);
        }

        public async Task UpdateStateAsync(int orderId, string status)
        {
            var orders = await _orderRepository.GetOrderByIdAsync(orderId);
            if(orders == null){
                throw new Exception("Order not found");
            }
            orders.Status = status;
            await _orderRepository.UpdateOrderAsync(orders);
        }

        public async Task<PaginatedResult<OrderDetailDto>> FilterOrderByStateAsync(string status, int page, int size)
        {
            
            var orders = await _orderRepository.FilterOrderByStateAsync(status,page,size);
            var orderDtos = _mapper.Map<IEnumerable<OrderDetailDto>>(orders.Items);

  
            return new PaginatedResult<OrderDetailDto>(orderDtos, orders.TotalCount, size);
        }

        public async Task CancelOrderAsync(int orderId, string userName)
        {

            var orders = await _orderRepository.GetOrderByIdAsync(orderId);
            if(orders == null){
                throw new Exception("Order not found");
            }
            var user = await _userRepository.GetUserByEmailAsync(userName);
            if(user == null){
                throw new Exception("User not found");
            }
            orders.Status = "Canceled";
            await _orderRepository.UpdateOrderAsync(orders);
        }

        public async Task<decimal> GetTotalRevenueByQuarterAsync(int year, int quarter)
        {
            return await _orderRepository.GetTotalRevenueByQuarterAsync(year, quarter);
        }

        public async Task<decimal> GetTotalRevenueByYearAsync(int year)
        {
            return await _orderRepository.GetTotalRevenueByYearAsync(year);
        }

        public async Task<PaginatedResult<OrderDetailDto>> SearchOrderAllAsync(FilterOrderDto filterOrderDto, int page, int size)
        {
            var orders =  await _orderRepository.SearchAllOrderAsync(filterOrderDto,page,size);
            var orderDto = _mapper.Map<IEnumerable<OrderDetailDto>>(orders.Items);
            return new PaginatedResult<OrderDetailDto>(orderDto, orders.TotalCount, size);
        }

        public async Task UpdateOrderInformationAsync(int orderId, UpdateOrderInformationDto updateOrderInformationDto)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if(order == null){
                throw new Exception("Order not found");
            }
             _mapper.Map(updateOrderInformationDto,order);
            await _orderRepository.UpdateOrderAsync(order);
        }
    }
}