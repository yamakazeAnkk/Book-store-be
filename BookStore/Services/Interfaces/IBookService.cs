using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Models;

namespace BookStore.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDetailsDto>> GetBooksPagedAsync(int pageNumber, int pageSize);
        Task<BookDetailsDto> GetBookByIdAsync(int bookId);
        Task<Book>AddBookAsync(BookDto bookDto);
        Task UpdateBookAsync(int id,BookDto bookDto);
        Task DeleteBookAsync(int bookId);
        Task<IEnumerable<BookDto>> SearchBooksByTitleAsync(string title, int page, int size);
        Task<IEnumerable<BookDto>> FilterBooksByBrandAsync(int brandId, int page, int size);
        Task<IEnumerable<BookDto>> SortBooksByPriceAsync(decimal min, decimal max, int page, int size);

        
    }
}