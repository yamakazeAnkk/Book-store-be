using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.DTOs;
using BookStore.Helper;
using BookStore.Models;
using BookStore.Repositories;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace BookStore.Services
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly FirebaseStorageService _firebaseStorageService;

        private readonly IFileUploadService _fileUploadService;
        private readonly IBrandRepository _brandRepository;

        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IUserRepository _userRepository;

        private readonly IHubContext<NotificationHub> _hubContext;

        public BookService(IHubContext<NotificationHub> hubContext,IUserRepository userRepository,IOrderItemRepository orderItemRepository,IFileUploadService fileUploadService, IBookRepository bookRepository, IMapper mapper, FirebaseStorageService firebaseStorageService,IBrandRepository brandRepository){
            _bookRepository = bookRepository;
            _mapper = mapper;
            _firebaseStorageService = firebaseStorageService;
            _brandRepository = brandRepository;
            _fileUploadService = fileUploadService;
            _hubContext = hubContext;
            _orderItemRepository = orderItemRepository;
            _userRepository = userRepository;
            
       
        }
       public async Task<Book> AddBookAsync(CreateBookDto bookDto, UploadFilesDto filesDto)
        {
            
            var book = _mapper.Map<Book>(bookDto);

         
            var createdBook = await _bookRepository.AddBookAsync(book);

        
            if (filesDto?.ImageFile != null && filesDto.ImageFile.Length > 0)
            {
               

                var fileName = Guid.NewGuid() + Path.GetExtension(filesDto.ImageFile.FileName);
                var imageUrl = await _fileUploadService.UploadImageAsync(filesDto.ImageFile);
                createdBook.Image = imageUrl;
            }

            // Xử lý tải lên eBook nếu có
            if (filesDto?.EbookFile != null && filesDto.EbookFile.Length > 0)
            {
                var ebookFileName = Guid.NewGuid() + Path.GetExtension(filesDto.EbookFile.FileName);
                var ebookUrl = await _fileUploadService.UploadEbookAsync(filesDto.EbookFile);
                createdBook.LinkEbook = ebookUrl;
            }

            // Kiểm tra nếu có BrandId nào được cung cấp
            if (bookDto.brandId != null && bookDto.brandId.Count > 0)
            {
              
                var bookBrands = bookDto.brandId
                    .Distinct() // Đảm bảo mỗi brandId chỉ thêm một lần
                    .Select(brandId => new BookBrand
                    {
                        BookId = createdBook.BookId,
                        BandId = brandId
                    })
                    .ToList();

              
                await _bookRepository.AddBookBrandAsync(bookBrands);
            }

            
            await _bookRepository.UpdateBookAsync(createdBook);

            return createdBook;
        }




        public async Task DeleteBookAsync(int bookId)
        {
            await _bookRepository.DeleteBookAsync(bookId);
        }

        public async Task<PaginatedResult<BookDto>> FilterBooksByBrandAsync(List<int> brandId, int page, int size)
        {
            var books = await _bookRepository.FilterBooksByBrandAsync(brandId,page,size);
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books.Items);

  
            return new PaginatedResult<BookDto>(bookDtos, books.TotalCount, size);
        }

        public async Task<BookDetailsDto> GetBookByIdAsync(int bookId)
        {
             var books = await _bookRepository.GetBookByIdAsync(bookId);
             return _mapper.Map<BookDetailsDto>(books);
        }

        public async Task<PaginatedResult<BookDetailsDto>> GetBooksPagedAsync(int pageNumber, int pageSize)
        {
            var books = await _bookRepository.GetBooksPagedAsync(pageNumber, pageSize);
            
            var bookDtos = _mapper.Map<IEnumerable<BookDetailsDto>>(books.Items);

   
            return new PaginatedResult<BookDetailsDto>(bookDtos, books.TotalCount, pageSize);
        }

        public async Task<PaginatedResult<BookDto>> SearchBooksByTitleAsync(string title, int page, int size)
        {
            var books = await _bookRepository.SearchBooksByTitleAsync(title,page,size);
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books.Items);

 
            return new PaginatedResult<BookDto>(bookDtos, books.TotalCount, size);
        }

        public async Task<PaginatedResult<BookDto>> SortBooksByPriceAsync(decimal min, decimal max, int page, int size)
        {
            var books = await _bookRepository.SortBooksByPriceAsync(min,max,page,size);
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books.Items);

   
             return new PaginatedResult<BookDto>(bookDtos, books.TotalCount, size);

        }

        public async Task UpdateBookAsync(int id,CreateBookDto bookDto,UploadFilesDto files)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if(book == null){
                throw new Exception("Book not found");
            }
            if (files?.ImageFile != null)
            {
                book.Image = await _fileUploadService.UploadImageAsync(files.ImageFile);
            }

            // Tải lên eBook nếu có
            if (files?.EbookFile != null)
            {
                book.LinkEbook = await _fileUploadService.UploadEbookAsync(files.EbookFile);
            }
            _mapper.Map(bookDto,book);
            if (string.IsNullOrEmpty(book.AuthorName))
            {
                book.AuthorName = "Unknown Author"; // hoặc một giá trị mặc định phù hợp
            }
         
            await _bookRepository.ClearBookBrandsAsync(id);
            await _bookRepository.AddBrandsToBookAsync(id,bookDto.brandId);

            await _bookRepository.UpdateBookAsync(book);
        }
        public async Task CheckBookQuantityAndNotifyAsync()
        {
            var lowStockBooks = await _bookRepository.GetBooksAsync(b => b.Quantity < 10);

            foreach (var book in lowStockBooks)
            {
                await _hubContext.Clients.All.SendAsync("NotifyAdmin", $"The book '{book.Title}' is low on stock (Quantity: {book.Quantity}).");
            }

            // Tính thời điểm 3 tháng trước
            var threeMonthsAgo = DateTime.Now.AddMonths(-3);

            // Lấy danh sách sách bị tồn kho
            var stagnantBooks = await _bookRepository.GetBooksAsync(b =>
                (b.UploadDate <= threeMonthsAgo && !b.OrderItems.Any()) ||
                (b.OrderItems.Any() && b.OrderItems.Max(oi => oi.Order.OrderDate) <= threeMonthsAgo)
            );

            foreach (var book in stagnantBooks)
            {
                await _hubContext.Clients.All.SendAsync("NotifyAdmin", $"The book '{book.Title}' has been stagnant for over 3 months and may need a discount.");
            }
        }

        public async Task<PaginatedResult<BookDto>> GetLowStockBooksAsync(int page, int size)
        {
            var books = await _bookRepository.GetLowStockBooksAsync(page,size);
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books.Items);

   
             return new PaginatedResult<BookDto>(bookDtos, books.TotalCount, size);
        }

        public async Task<PaginatedResult<BookDto>> GetStagnantBooksAsync(int page, int size)
        {
            var books = await _bookRepository.GetStagnantBooksAsync(page,size);
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books.Items);

   
             return new PaginatedResult<BookDto>(bookDtos, books.TotalCount, size);
        }

        public async Task<PaginatedResult<BookDto>> SearchAllBooksAsync(string title, List<int> brandIds, int page, int size)
        {
            var books = await _bookRepository.SearchAllBookAsync(title,brandIds,page,size);
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books.Items);

 
            return new PaginatedResult<BookDto>(bookDtos, books.TotalCount, size);
        }

        public async Task<IEnumerable<BookDto>> GetTopBooksAsync(int topCount)
        {
            var book = await _bookRepository.GetTopBooksAsync(topCount);
            return _mapper.Map<IEnumerable<BookDto>>(book);
        }

        public async Task<IEnumerable<BookDto>> GetLatestBooksAsync(int latestCount)
        {
            var book = await _bookRepository.GetLatestBooksAsync(latestCount);
            return _mapper.Map<IEnumerable<BookDto>>(book);
        }

        public async Task UpdateIsSaleBookAsync(int id,int isSale)
        {
            await _bookRepository.UpdateIsSaleBookAsync(id,isSale);
        }

        public async Task<IEnumerable<BookDto>> GetBestSellerAsync(int bestCount)
        {
            var bestSellers = await _orderItemRepository.GetBestSellersAsync();

            return bestSellers
                .Take(bestCount)
                .Select(pc => new BookDto
                {
                    BookId = pc.Book.BookId,
                    Title = pc.Book.Title,
                    AuthorName = pc.Book.AuthorName,
                    Image = pc.Book.Image,
                    Price = pc.Book.Price,
                    Rating = pc.Book.Rating,
                    
                });
        }

        public async Task<PaginatedResult<BookDto>> FilterBookPurchasedBookByUserAsync(string email, int page, int size)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if(user == null){
                throw new Exception("User not found");
            }
            var books = await _bookRepository.FilterBookPurchasedBookByUserAsync(user.UserId,page,size);
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books.Items);

 
            return new PaginatedResult<BookDto>(bookDtos, books.TotalCount, size);
        }

        public async Task<PaginatedResult<BookDto>> FilterTypeBookAsync(int id, int page, int size)
        {
            var books = await _bookRepository.FilterTypeBookAsync(id,page,size);
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books.Items);

 
            return new PaginatedResult<BookDto>(bookDtos, books.TotalCount, size);
        }
    }
}