using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookStoreContext _bookStoreContext;

        public UserRepository(BookStoreContext bookStoreContext){
            _bookStoreContext = bookStoreContext;
        }
        public async Task AddUserAsync(User user)
        {
            await _bookStoreContext.Users.AddAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _bookStoreContext.Users.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllUserAsync(int page, int size)
        {
            return await _bookStoreContext.Users.Skip((page - 1) * size).Take(page).ToListAsync();
        }

        public async Task<Role> GetRoleByNameAsync(string role)
        {
            return await _bookStoreContext.Roles.SingleOrDefaultAsync(u => u.RoleName == role);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _bookStoreContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await  _bookStoreContext.Users.FindAsync(userId);
        }

        public async Task<User> GetUserByNameAsync(string username)
        {
            return await _bookStoreContext.Users.Include(u  => u.Role).FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _bookStoreContext.SaveChangesAsync() > 0;
        }

        public async Task UpdateUserByAsync(User user)
        {
            _bookStoreContext.Users.Update(user);
            await _bookStoreContext.SaveChangesAsync();
        }
    }
}