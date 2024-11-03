using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Helper;
using BookStore.Models;
using PagedList;

namespace BookStore.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(int? bookId);
        
        Task<PaginatedResult<Book>> GetBooksPagedAsync(int pageNumber, int pageSize);

        Task<Book> AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);

        Task DeleteBookAsync (int id);
        Task AddBookBrandAsync(List<BookBrand> bookBrand);

        
       

        Task<PaginatedResult<Book>> SearchBooksByTitleAsync(string title, int page, int size);
        Task<PaginatedResult<Book>> FilterBooksByBrandAsync(List<int> brandIds, int page, int size);
        Task<PaginatedResult<Book>> SortBooksByPriceAsync(decimal min, decimal max, int page, int size);
        
    }
}