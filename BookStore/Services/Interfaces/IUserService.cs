using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Helper;
using BookStore.Models;

namespace BookStore.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(UserDto userDto);

        Task<User> LoginUserAsync(LoginDto loginDto);

        Task UpdateIsActionUserAsync(int id);

        Task<User> GetUserByEmailAsync(string email);

        Task<PaginatedResult<UserDetailDto>> GetUserAllAsync(int page, int size);
        Task<PaginatedResult<UserDetailDto>> FilterByUserAsync(FilterUserDto filterUserDto,int page,int size);

        Task UpdateUserAsync(int id, CreateUserDetailDto createUserDetailDto);

        

        
    }
}