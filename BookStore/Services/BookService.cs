using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.Repositories;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;

namespace BookStore.Services
{
    public class BookService : IBookService
    {

        
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly FirebaseStorageService _firebaseStorageService;

        private readonly IBrandRepository _brandRepository;

        public BookService( IBookRepository bookRepository, IMapper mapper, FirebaseStorageService firebaseStorageService,IBrandRepository brandRepository){
            _bookRepository = bookRepository;
            _mapper = mapper;
            _firebaseStorageService = firebaseStorageService;
            _brandRepository = brandRepository;
       
        }
       public async Task<Book> AddBookAsync(BookDto bookDto)
        {
            // Ánh xạ BookDto sang Book
            var book = _mapper.Map<Book>(bookDto);

            // Thêm Book vào cơ sở dữ liệu
            var createdBook = await _bookRepository.AddBookAsync(book);

            // Kiểm tra nếu có BrandId nào được cung cấp
            if (bookDto.brandId != null && bookDto.brandId.Count > 0)
            {
                // Khởi tạo một danh sách các BookBrand để thêm vào một lần
                var bookBrands = bookDto.brandId
                    .Distinct() // Đảm bảo mỗi brandId chỉ thêm một lần
                    .Select(brandId => new BookBrand
                    {
                        BookId = createdBook.BookId,
                        BandId = brandId
                    })
                    .ToList();

             
                
            }

            return createdBook;
        }



        public async Task DeleteBookAsync(int bookId)
        {
            await _bookRepository.DeleteBookAsync(bookId);
        }

        public async Task<IEnumerable<BookDto>> FilterBooksByBrandAsync(int brandId, int page, int size)
        {
            var books = await _bookRepository.FilterBooksByBrandAsync(brandId,page,size);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDetailsDto> GetBookByIdAsync(int bookId)
        {
             var books = await _bookRepository.GetBookByIdAsync(bookId);
             return _mapper.Map<BookDetailsDto>(books);
        }

        public async Task<IEnumerable<BookDetailsDto>> GetBooksPagedAsync(int pageNumber, int pageSize)
        {
            var books = await _bookRepository.GetBooksPagedAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<BookDetailsDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> SearchBooksByTitleAsync(string title, int page, int size)
        {
            var books = await _bookRepository.SearchBooksByTitleAsync(title,page,size);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> SortBooksByPriceAsync(decimal min, decimal max, int page, int size)
        {
            var books = await _bookRepository.SortBooksByPriceAsync(min,max,page,size);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task UpdateBookAsync(int id,BookDto bookDto)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if(book == null){
                throw new Exception("Book not found");
            }
            _mapper.Map(bookDto,book);

            await _bookRepository.UpdateBookAsync(book);
        }
    }
}