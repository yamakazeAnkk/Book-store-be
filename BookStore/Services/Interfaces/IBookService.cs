using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;

namespace BookStore.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBooksPagedAsync(int pageNumber, int pageSize);
        Task<BookDto> GetBookByIdAsync(int bookId);
        Task AddBookAsync(CreateBookDto bookDto,string? imageFile);
        Task UpdateBookAsync(int id,BookDto bookDto);
        Task DeleteBookAsync(int bookId);
    }
}