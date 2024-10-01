using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> GetAuthorByIdAsync(int authorId);

        // Lấy tất cả các Brands
        Task<IEnumerable<Author>> GetAllAuthorsAsync();

        // Thêm Brand mới
        Task<Author> AddAuthorAsync(Author author);

        // Cập nhật Brand
        Task UpdateAuthorAsync(Author author);

        // Xóa Brand
        Task DeleteAuthorAsync(int authorId);
    }
}