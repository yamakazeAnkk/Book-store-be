using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Helper;
using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);

        Task<User> GetUserByNameAsync(string username);


        Task<PaginatedResult<User>> GetAllUserAsync(int page , int size);

        Task AddUserAsync(User user);

        Task<bool> SaveChangeAsync();

        Task<Role> GetRoleByNameAsync(string role);

        Task<User> GetUserByEmailAsync(string email);

        Task UpdateUserByAsync(User user);

        Task UpdateIsActionUserAsync(int id,int isAction);

        Task<bool> ChangePasswordAsync(int userId, string newPassword);

        Task <PaginatedResult<User>> FilterByUserAsync(FilterUserDto filterUserDto,int page,int size);


        

    }
}