using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Schema;
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
                book.UploadDate = DateTime.Now; 
            }

            book.IsSale = 1;
            await _bookStoreContext.Books.AddAsync(book);
            await _bookStoreContext.SaveChangesAsync();

            return book;
        }

     

        public async Task AddBookBrandAsync(List<BookBrand> bookBrand)
        {
             _bookStoreContext.BookBrands.AddRangeAsync(bookBrand);
            await _bookStoreContext.SaveChangesAsync();
        }

        public async Task AddBrandsToBookAsync(int bookId, List<int> brandIds)
        {
            if(brandIds == null || !brandIds.Any())
                return ;
            
            foreach (var branId in brandIds)
            {
                _bookStoreContext.BookBrands.Add(new BookBrand {BookId = bookId, BandId = branId});
            }
            await _bookStoreContext.SaveChangesAsync();

        }

        public async Task ClearBookBrandsAsync(int bookId)
        {
            var bookBrand = _bookStoreContext.BookBrands.Where(c => c.BookId == bookId).ToList();

            _bookStoreContext.BookBrands.RemoveRange(bookBrand);
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

        public async Task<PaginatedResult<Book>> FilterBookPurchasedBookByUserAsync(int id, int page, int size)
        {
            var query = _bookStoreContext.PurchasedEbooks
                .Where(pe => pe.UserId == id && pe.Book != null) 
                .Include(pe => pe.Book)
                .Select(pe => pe.Book)
                .AsEnumerable()
                .GroupBy(book => book.BookId) 
                .Select(group => group.FirstOrDefault()) 
                .Where(book => book != null);

            var totalCount =  query.Count();

            var books =  query
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();
            return new PaginatedResult<Book>(books,totalCount,size);

        }

        public async Task<PaginatedResult<Book>> FilterBooksByBrandAsync(List<int> brandIds, int page, int size)
        {
            int totalCount = await _bookStoreContext.BookBrands
            .Where(bb => brandIds.Contains(bb.BandId.GetValueOrDefault()) )
            .Select(bb => bb.Book.BookId)
            .Distinct()
            .CountAsync();
            var books=  await _bookStoreContext.BookBrands
                .Where(bb => brandIds.Contains(bb.BandId.GetValueOrDefault())  )
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

        public async Task<PaginatedResult<Book>> FilterTypeBookAsync(int id, int page, int size)
        {
            var query = _bookStoreContext.Books.Where(b => b.TypeBookId == id);
            var totalCount = await query.CountAsync();

            var books = await query
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

        public async Task<IEnumerable<Book>> GetLatestBooksAsync(int latestCount)
        {
            return await _bookStoreContext.Books
            .Where(b => b.IsSale == 1 )
            .OrderByDescending(b => b.UploadDate)        
            .Take(latestCount)                           
            .ToListAsync();
        }

        public async Task<PaginatedResult<Book>> GetLowStockBooksAsync(int page, int size)
        {
            var query = _bookStoreContext.Books
                .Where(b => b.Quantity <= 10);

            int totalCount = await query.CountAsync();

            var lowStockBooks = await query
                .Where(b => b.IsSale == 1)
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
                
                    // Sách chưa từng được order
                    (!b.OrderItems.Any() && b.UploadDate <= threeMonthsAgo) ||
                    // Trong OrderItems quá 3 tháng
                    (b.OrderItems.Any() && b.OrderItems.Max(oi => oi.Order.OrderDate) <= threeMonthsAgo)
                  
                );

            int totalCount = await query.CountAsync();

            var stagnantBooks = await query
                .OrderBy(b => b.Title) 
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginatedResult<Book>(stagnantBooks, totalCount, size);
        }

        public async Task<IEnumerable<Book>> GetTopBooksAsync(int topCount)
        {
            return await _bookStoreContext.Books
            .Where(b => b.IsSale == 1  && b.Rating.HasValue )
            .OrderByDescending(b => b.Rating)               
            .Take(topCount)                                 
            .ToListAsync();
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

        public async Task<PaginatedResult<Book>> SearchBooksByTitleAsync(string? searchTerm, int page, int size)
        {
            var query = _bookStoreContext.Books.Where(b => b.IsSale == 1);
            
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(b => EF.Functions.Like(b.Title, $"%{searchTerm}%") || EF.Functions.Like(b.AuthorName, $"%{searchTerm}%"));
            }
            int totalCount = await query.CountAsync();

            var books = await query
              
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
              
            }
            entry.Property(b => b.UploadDate).IsModified = true;
            await _bookStoreContext.SaveChangesAsync();

        }

        public async Task UpdateIsSaleBookAsync(int id,int isSale)
        {
            var book = await _bookStoreContext.Books.FindAsync(id);
            if(book != null){
                book.IsSale = isSale;
                await _bookStoreContext.SaveChangesAsync();
            }else{
                throw new Exception("Book not found");
            }
        }
    }
}  