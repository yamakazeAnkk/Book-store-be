using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.DTOs;
using BookStore.Helper;
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

        public async Task<PaginatedResult<UserDetailDto>> GetUserAllAsync(int page, int size)
        {
            
            var users = await _userRepository.GetAllUserAsync(page,size);
            
            var userDtos = _mapper.Map<IEnumerable<UserDetailDto>>(users.Items);

            // Tạo đối tượng `PaginatedResult` mới với DTOs
            return new PaginatedResult<UserDetailDto>(userDtos, users.TotalCount, size);
        }
       

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
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

        public async Task UpdateIsActionUserAsync(int id)
        {
            await _userRepository.UpdateIsActionUserAsync(id);
        }

        public async Task UpdateUserAsync(int id, CreateUserDetailDto createUserDetailDto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if(user == null || user.RoleId != 2 && user.RoleId != 3 ){
                throw new Exception("User not found");
            }
            _mapper.Map(createUserDetailDto,user);
            await _userRepository.UpdateUserByAsync(user);

            
        }
    }
}