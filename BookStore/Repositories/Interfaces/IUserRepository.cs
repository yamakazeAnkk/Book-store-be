using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);

        Task<User> GetUserByNameAsync(string username);

        Task<IEnumerable<User>> GetAllUserAsync();

        Task AddUserAsync(User user);

        Task<bool> SaveChangeAsync();

        Task<Role> GetRoleByNameAsync(string role);

    }
}