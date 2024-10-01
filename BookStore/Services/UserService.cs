using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.Repositories;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;

namespace BookStore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        

        public UserService(IUserRepository userRepository, IMapper mapper){
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        public async Task<User> LoginUserAsync(LoginDto loginDto)
        {
            
            var user = await _userRepository.GetUserByNameAsync(loginDto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return null; // Invalid login
            }

            return user; // Success
        }

        public async Task<User> RegisterUserAsync(UserDto userDto)
        {
            var userExists = await _userRepository.GetUserByNameAsync(userDto.Username);
            if(userExists != null){
                throw new ArgumentException("Username already exists.");
            }
      

            var user = _mapper.Map<User>(userDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            user.RoleId = 3;
            
            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangeAsync();
            return user;
            
        }
    }
}