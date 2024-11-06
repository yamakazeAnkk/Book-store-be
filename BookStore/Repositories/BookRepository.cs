using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookStore.Helper;
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

        public async Task<PaginatedResult<Book>> FilterBooksByBrandAsync(List<int> brandIds, int page, int size)
        {
            int totalCount = await _bookStoreContext.BookBrands
            .Where(bb => brandIds.Contains(bb.BandId.GetValueOrDefault()))
            .Select(bb => bb.Book.BookId)
            .Distinct()
            .CountAsync();
            var books=  await _bookStoreContext.BookBrands
                .Where(bb => brandIds.Contains(bb.BandId.GetValueOrDefault()))
                .Include(bb => bb.Book)
                .ThenInclude(b => b.BookBrands)
                .ThenInclude(bb => bb.Band)
                .GroupBy(bb => bb.Book.BookId) // Nhóm theo `BookId` để loại bỏ trùng lặp
                .Select(g => g.First().Book)   // Lấy `Book` đầu tiên từ mỗi nhóm
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            return new PaginatedResult<Book>(books,totalCount,size);
        }

        public async Task<Book> GetBookByIdAsync(int? bookId)
        {
            if (bookId == null)
            {
                return null;
            }
            return await _bookStoreContext.Books.Include(u => u.BookBrands).ThenInclude(x=> x.Band).FirstOrDefaultAsync(b => b.BookId == bookId);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(Expression<Func<Book, bool>> predicate)
        {
            return await _bookStoreContext.Books.Where(predicate).ToListAsync();
        }

        public async Task<PaginatedResult<Book>> GetBooksPagedAsync(int pageNumber, int pageSize)
        {
            int totalCount = await _bookStoreContext.Books.CountAsync();
            var books = await _bookStoreContext.Books
                .Include(u => u.BookBrands)
                .ThenInclude(x => x.Band)
                .OrderBy(b => b.Title) // Sắp xếp theo tiêu đề
                .Skip((pageNumber - 1) * pageSize) // Bỏ qua các trang trước đó
                .Take(pageSize) // Lấy số lượng bản ghi cho trang hiện tại
                .ToListAsync();
            return new PaginatedResult<Book>(books, totalCount, pageSize);
        }

        public async Task<PaginatedResult<Book>> GetLowStockBooksAsync(int page, int size)
        {
            var query = _bookStoreContext.Books
                .Where(b => b.Quantity <= 10);

            int totalCount = await query.CountAsync();

            var lowStockBooks = await query
                .OrderBy(b => b.Title) 
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginatedResult<Book>(lowStockBooks, totalCount, size);
        }

        public async Task<PaginatedResult<Book>> GetStagnantBooksAsync(int page, int size)
        {
            var threeMonthsAgo = DateTime.Now.AddMonths(-3);

            var query = _bookStoreContext.Books
                .Where(b =>
                    (b.UploadDate <= threeMonthsAgo) || // 3 tháng từ ngày upload
                    !b.OrderItems.Any() || // Sách chưa từng được order
                    b.OrderItems.Max(oi => oi.Order.OrderDate) <= threeMonthsAgo // 3 tháng từ lần order gần nhất
                );

            int totalCount = await query.CountAsync();

            var stagnantBooks = await query
                .OrderBy(b => b.Title) // Có thể thay đổi để sắp xếp theo thuộc tính khác
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginatedResult<Book>(stagnantBooks, totalCount, size);
        }

        public async Task<PaginatedResult<Book>> SearchAllBookAsync(string title, List<int> brandIds, int page, int size)
        {
            var query = _bookStoreContext.Books.AsQueryable();


            if ((!string.IsNullOrEmpty(title) && brandIds == null) || brandIds.Count == 0)
            {
                query = query.Where(b => EF.Functions.Like(b.Title, $"%{title}%") ||  EF.Functions.Like(b.AuthorName, $"%{title}%" ));
            }else if (string.IsNullOrEmpty(title) && (brandIds != null || brandIds.Count > 0))
            {   
               return await FilterBooksByBrandAsync(brandIds, page, size);
            }else if ((!string.IsNullOrEmpty(title) && brandIds != null) || brandIds.Count > 0){
                query = query.Where(b => b.BookBrands.Any(bb => brandIds.Contains(bb.BandId.GetValueOrDefault())) && EF.Functions.Like(b.Title, $"%{title}%") ||  EF.Functions.Like(b.AuthorName, $"%{title}%" ));
            }
            
            int totalCount = await query.CountAsync();
            var books = await query
            .Include(u => u.BookBrands)
                .ThenInclude(x => x.Band)
                .OrderBy(b => b.Title) 
                .Skip((page - 1) * size) 
                .Take(size) 
                .ToListAsync();
            return new PaginatedResult<Book>(books,totalCount,size);
        }

        public async Task<PaginatedResult<Book>> SearchBooksByTitleAsync(string searchTerm, int page, int size)
        {
            int totalCount = await _bookStoreContext.Books
                .CountAsync(b => EF.Functions.Like(b.Title, $"%{searchTerm}%") || EF.Functions.Like(b.AuthorName, $"%{searchTerm}%"));

            var books = await _bookStoreContext.Books
                .Where(b => EF.Functions.Like(b.Title, $"%{searchTerm}%") || EF.Functions.Like(b.AuthorName, $"%{searchTerm}%"))
                .OrderBy(b => b.Title)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginatedResult<Book>(books, totalCount, size);
            
        }

        public async Task<PaginatedResult<Book>> SortBooksByPriceAsync(decimal min, decimal max, int page, int size)
        {
            int totalCount = await _bookStoreContext.Books
                .CountAsync(b => b.Price >= min && b.Price <= max);

            var books = await _bookStoreContext.Books
                .Where(b => b.Price >= min && b.Price <= max)
                .OrderBy(b => b.Price)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginatedResult<Book>(books, totalCount, size);
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