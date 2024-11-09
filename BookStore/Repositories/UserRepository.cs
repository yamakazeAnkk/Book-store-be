using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Helper;
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
            user.IsActive = 1;
            await _bookStoreContext.Users.AddAsync(user);
        }

        public async Task<PaginatedResult<User>> GetAllUserAsync(int page, int size)
        {
            int totalCount = await _bookStoreContext.Users
                                            .CountAsync(user => user.RoleId != 1);
            var users = await _bookStoreContext.Users.Where(user => user.RoleId != 1).OrderBy(user => user.UserId).Skip((page - 1) * size).Take(size).ToListAsync();
            
            return new PaginatedResult<User>(users,totalCount,size);
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

        public async Task UpdateIsActionUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if(user != null){
                user.IsActive = 0;
                await _bookStoreContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserByAsync(User user)
        {
            _bookStoreContext.Users.Update(user);
            await _bookStoreContext.SaveChangesAsync();
        }
    }
}