using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Models;

namespace BookStore.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(UserDto userDto);

        Task<User> LoginUserAsync(LoginDto loginDto);
    }
}