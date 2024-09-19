using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using PagedList;

namespace BookStore.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<Book> GetBookByIdAsync(int bookId);
        
        Task<IEnumerable<Book>> GetBooksPagedAsync(int pageNumber, int pageSize);

        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);

        Task DeleteBookAsync (int id);
    }
}