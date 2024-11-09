using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.DTOs;
using BookStore.Services;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace BookStore.Controllers
{
    [ApiController]
    
    [Route("api/[controller]")]
    [Authorize(Roles = "user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService; 

        public UserController(IUserService userService ){
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUserPaged(int pageNumber = 1, int pageSize = 10)
        {
            var pageUsers =  await _userService.GetUserAllAsync(pageNumber, pageSize);
            return Ok(pageUsers);
        } 
        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            var emailUser = User.FindFirst(ClaimTypes.Email)?.Value;
                if (string.IsNullOrEmpty(emailUser))
                    return Unauthorized("User not authenticated");
            
            var user = await _userService.GetUserByEmailAsync(emailUser);
            return Ok(user);
        } 
       
        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] CreateUserDetailDto createUserDetailDto)
        {
            try
            {
                await _userService.UpdateUserAsync(id, createUserDetailDto);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); 
            }
        } 
        
    }

}