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

        public BookService(IBookRepository bookRepository, IMapper mapper, FirebaseStorageService firebaseStorageService,IBrandRepository brandRepository){
            _bookRepository = bookRepository;
            _mapper = mapper;
            _firebaseStorageService = firebaseStorageService;
            _brandRepository = brandRepository;
        }
        public async Task<Book> AddBookAsync(BookDto bookDto)
        {
           var book = _mapper.Map<Book>(bookDto);

           var createBook = await _bookRepository.AddBookAsync(book);
           if(bookDto.brandId != null && bookDto.brandId.Count > 0){
                foreach (var brandId in bookDto.brandId)
                {
                    var brand = await _brandRepository.GetBrandByIdAsync(brandId);
                    if(brand != null){
                        var bookBrand = new BookBrand{
                            BookId = createBook.BookId,
                            BandId = brand.BrandId
                        };
                        await _bookRepository.AddBookBrandAsync(bookBrand);
                    }
                }
           }
           return createBook;
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