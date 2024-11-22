using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Helper;
using BookStore.Models;

namespace BookStore.Services.Interfaces
{
    public interface IBookService
    {
        Task<PaginatedResult<BookDetailsDto>> GetBooksPagedAsync(int pageNumber, int pageSize);
        Task<BookDetailsDto> GetBookByIdAsync(int bookId);
        Task<Book>AddBookAsync(CreateBookDto bookDto,UploadFilesDto filesDto);
        Task UpdateBookAsync(int id,CreateBookDto bookDto,UploadFilesDto filesDto);
        Task DeleteBookAsync(int bookId);

        Task UpdateIsSaleBookAsync(int id,int isSale);

        Task CheckBookQuantityAndNotifyAsync();
        Task<PaginatedResult<BookDto>> GetLowStockBooksAsync(int page, int size);
        Task<PaginatedResult<BookDto>> GetStagnantBooksAsync(int page, int size);
        Task<PaginatedResult<BookDto>> SearchBooksByTitleAsync(string? title, int page, int size);
        Task<PaginatedResult<BookDto>> SearchAllBooksAsync(string title,List<int> brandIds, int page, int size);
        Task<PaginatedResult<BookDto>> FilterBooksByBrandAsync(List<int> brandIds, int page, int size);

        Task<PaginatedResult<BookDto>> SortBooksByPriceAsync(decimal min, decimal max, int page, int size);

        Task<IEnumerable<BookDto>> GetTopBooksAsync(int topCount);
        Task<IEnumerable<BookDto>> GetLatestBooksAsync(int latestCount);

        Task<IEnumerable<BookDto>> GetBestSellerAsync(int bestCount);

        Task<PaginatedResult<PurchasedBookDto>> FilterBookPurchasedBookByUserAsync(string email,int page ,int size);
        Task<PaginatedResult<BookDto>> FilterTypeBookAsync(int id,int page ,int size);
        
    }
}