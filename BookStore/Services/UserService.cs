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
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        private readonly IEmailService _emailService;

        private readonly Random _random;

        public UserService(IUserRepository userRepository, IMapper mapper, IEmailService emailService, Random random){
            _userRepository = userRepository;
            _mapper = mapper;
            _random = random;
            _emailService =  emailService;
        }

        public async Task<bool> ChangePasswordAsync(string email,string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
                throw new ArgumentException("Mật khẩu không hợp lệ.");
            var user = await _userRepository.GetUserByEmailAsync(email);
            if(user == null ) throw new Exception("Người dùng không tồn tại.");

            var isOldPasswordCorrect = BCrypt.Net.BCrypt.Verify(oldPassword, user.Password);
            if (!isOldPasswordCorrect)
                 throw new Exception("Mật khẩu cũ không chính xác.");

            var hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.Password = hashedNewPassword;

            return await _userRepository.ChangePasswordAsync(user); 
        }

        public async Task<PaginatedResult<UserDetailDto>> FilterByUserAsync(FilterUserDto filterUserDto, int page, int size)
        {
            var user = await _userRepository.FilterByUserAsync(filterUserDto,page,size);
            var userDto = _mapper.Map<IEnumerable<UserDetailDto>>(user.Items);

            return new PaginatedResult<UserDetailDto>(userDto,user.TotalCount,size);
        }

        public async Task<string> GenerateAndSendTokenAsync(string email)
        {
            var token = _random.Next(100000,999999).ToString();
            string subject = "Your Verification Token";
            string body = $"Your verification token is: {token}";

            await _emailService.SendEmailAsync(email,subject,body);
            return token;
        }

        public async Task<PaginatedResult<UserDetailDto>> GetUserAllAsync(int page, int size)
        {
            
            var users = await _userRepository.GetAllUserAsync(page,size);
            
            var userDtos = _mapper.Map<IEnumerable<UserDetailDto>>(users.Items);

            // Tạo đối tượng `PaginatedResult` mới với DTOs
            return new PaginatedResult<UserDetailDto>(userDtos, users.TotalCount, size);
        }
       

        public async Task<UserPrivateDto> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            // _mapper.Map<UserPrivateDto>(user);
            return _mapper.Map<UserPrivateDto>(user);

        }

        public async Task<User> LoginUserAsync(LoginDto loginDto)
        {
            
            var user = await _userRepository.GetUserByNameAsync(loginDto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return null;
            }

            return user; // Success
        }

        public async Task<User> RegisterUserAsync(UserDto userDto)
        {
            var existsEmail = await _userRepository.GetUserByEmailAsync(userDto.Email);
            var existsName = await _userRepository.GetUserByNameAsync(userDto.Username);
            if(existsEmail != null || existsName != null){
                throw new ArgumentException("Username already exists.");
            }
      

            var user = _mapper.Map<User>(userDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            user.RoleId = 3;
            
            await _userRepository.AddUserAsync(user);
          
            return user;
            
        }

        public async Task UpdateIsActionUserAsync(int id,int isAction)
        {
            await _userRepository.UpdateIsActionUserAsync(id,isAction);
        }

        public async Task UpdateAdminAsync(int id, CreateUserDetailDto createUserDetailDto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if(user == null || user.RoleId != 2 && user.RoleId != 3 ){
                throw new Exception("User not found");
            }
            _mapper.Map(createUserDetailDto,user);
            await _userRepository.UpdateUserByAsync(user);

            
        }

        public async Task<bool> UpdateUserAsync(string email, UpdateUserDto updateUserDto,string imageUrl)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if(user == null){
                throw new Exception("User not found");
            }
            _mapper.Map(updateUserDto,user);
            user.ProfileImage = imageUrl;
            await _userRepository.UpdateUserByAsync(user);
            return true;
        }

       

        public Task<bool> UpdateUserAsync(string email, UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}