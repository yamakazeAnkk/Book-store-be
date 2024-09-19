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
        public async Task AddBookAsync(Book book)
        {
            if (book.UploadDate == DateTime.MinValue)
            {
                book.UploadDate = DateTime.Now;  // Hoặc bạn có thể đặt một ngày cụ thể hợp lệ
            }
            await _bookStoreContext.Books.AddAsync(book);
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

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _bookStoreContext.Books.FindAsync(bookId);
        }



        public async Task<IEnumerable<Book>> GetBooksPagedAsync(int pageNumber, int pageSize)
        {
            return await _bookStoreContext.Books
                .OrderBy(b => b.Title) // Sắp xếp theo tiêu đề
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các trang trước đó
                .Take(pageSize) // Lấy số lượng bản ghi cho trang hiện tại
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