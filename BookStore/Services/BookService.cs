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
    public class BookService : IBookService
    {

        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper){
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task AddBookAsync(CreateBookDto bookDto,string? imageFile)
        {
           var book = _mapper.Map<Book>(bookDto);
           if (!string.IsNullOrEmpty(imageFile))
            {
                book.Image = imageFile;
            }
           await _bookRepository.AddBookAsync(book);
        }

        public async Task DeleteBookAsync(int bookId)
        {
            await _bookRepository.DeleteBookAsync(bookId);
        }

        public async Task<BookDto> GetBookByIdAsync(int bookId)
        {
             var books = await _bookRepository.GetBookByIdAsync(bookId);
             return _mapper.Map<BookDto>(books);
        }

        public async Task<IEnumerable<BookDto>> GetBooksPagedAsync(int pageNumber, int pageSize)
        {
            var books = await _bookRepository.GetBooksPagedAsync(pageNumber, pageSize);
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