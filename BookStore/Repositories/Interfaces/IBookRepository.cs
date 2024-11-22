using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        Task<IEnumerable<Book>> GetBooksAsync(Expression<Func<Book, bool>> predicate);

        Task<PaginatedResult<Book>> GetLowStockBooksAsync(int page ,int size);
        Task<PaginatedResult<Book>> GetStagnantBooksAsync(int page ,int size);
        
        Task ClearBookBrandsAsync(int bookId);
        
        Task AddBrandsToBookAsync(int bookId,List<int> brandIds);

        Task<PaginatedResult<Book>> SearchBooksByTitleAsync(string? title, int page, int size);
        Task<PaginatedResult<Book>> FilterBooksByBrandAsync(List<int> brandIds, int page, int size);
        Task<PaginatedResult<Book>> SortBooksByPriceAsync(decimal min, decimal max, int page, int size);

        Task<PaginatedResult<Book>> SearchAllBookAsync(string title,List<int> brandIds,int page, int size);

        Task<IEnumerable<Book>> GetTopBooksAsync(int topCount);

        Task<IEnumerable<Book>> GetLatestBooksAsync(int latestCount);


        Task UpdateIsSaleBookAsync(int id,int isSale);

        Task<PaginatedResult<Book>> FilterBookPurchasedBookByUserAsync(int id,int page ,int size);
        Task<PaginatedResult<Book>> FilterTypeBookAsync(int id,int page ,int size);
        
    }
}