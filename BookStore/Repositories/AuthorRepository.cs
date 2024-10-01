using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repositories.Interfaces;

namespace BookStore.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookStoreContext _context;
        public AuthorRepository(BookStoreContext context){
            _context = context;
        }

        public Task<Author> AddAuthorAsync(Author author)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAuthorAsync(int authorId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Author> GetAuthorByIdAsync(int authorId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAuthorAsync(Author author)
        {
            throw new NotImplementedException();
        }
    }
}