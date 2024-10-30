using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using PagedList;
using PagedList.Core;

namespace BookStore.Repositories
{
    public class BookRepository : IBookRepository
    {

        private readonly BookStoreContext _bookStoreContext;

        public BookRepository(BookStoreContext bookStoreContext){
            _bookStoreContext = bookStoreContext;
        }
        public async Task<Book> AddBookAsync(Book book)
        {
            if (book.UploadDate == DateTime.MinValue)
            {
                book.UploadDate = DateTime.Now;  // Hoặc bạn có thể đặt một ngày cụ thể hợp lệ
            }

            
            await _bookStoreContext.Books.AddAsync(book);
            await _bookStoreContext.SaveChangesAsync();

            return book;
        }

     

        public async Task AddBookBrandAsync(List<BookBrand> bookBrand)
        {
             _bookStoreContext.BookBrands.AddRangeAsync(bookBrand);
            await _bookStoreContext.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if(book != null){
                _bookStoreContext.Remove(id);
                await _bookStoreContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> FilterBooksByBrandAsync(int brandId, int page, int size)
        {
            return await _bookStoreContext.BookBrands
                .Where(bb => bb.BandId == brandId)
                .Include(bb => bb.Book)
                .Select(bb => bb.Book)
                .Skip((page-1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int? bookId)
        {
            if (bookId == null)
            {
                return null;
            }
            return await _bookStoreContext.Books.Include(u => u.BookBrands).ThenInclude(x=> x.Band).FirstOrDefaultAsync(b => b.BookId == bookId);
        }

    

        public async Task<IEnumerable<Book>> GetBooksPagedAsync(int pageNumber, int pageSize)
        {
            return await _bookStoreContext.Books
                .Include(u => u.BookBrands)
                .ThenInclude(x => x.Band)
                .OrderBy(b => b.Title) // Sắp xếp theo tiêu đề
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các trang trước đó
                .Take(pageSize) // Lấy số lượng bản ghi cho trang hiện tại
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchBooksByTitleAsync(string searchTerm, int page, int size)
        {
            return await _bookStoreContext.Books
                .Where(b => EF.Functions.Like(b.Title,$"%{searchTerm}%") || EF.Functions.Like(b.AuthorName,$"%{searchTerm}%") )
                .Skip((page-1) * size)
                .Take(size)
                .ToListAsync();
            
        }

        public async Task<IEnumerable<Book>> SortBooksByPriceAsync(decimal min, decimal max, int page, int size)
        {
            return await _bookStoreContext.Books
            .Where(b => b.Price >= min && b.Price <= max) // Filter by price range
            .OrderBy(b => b.Price) // Sort by price in ascending order
            .Skip((page - 1) * size) // Pagination
            .Take(size)
            .ToListAsync();
        }

        public async Task UpdateBookAsync(Book book)
        {
            book.UploadDate = DateTime.Now;
            var entry = _bookStoreContext.Entry(book);
            
            if(entry.State == EntityState.Detached){
                _bookStoreContext.Books.Attach(book);
                entry.State = EntityState.Modified;
            }
            await _bookStoreContext.SaveChangesAsync();

        }
    }
}  