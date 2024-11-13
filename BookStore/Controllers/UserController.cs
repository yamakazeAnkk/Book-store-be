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
    
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService; 

        public UserController(IUserService userService ){
            _userService = userService;
        }
        [HttpGet("all-user")]
        public async Task<IActionResult> GetAllUserPaged(int pageNumber = 1, int pageSize = 10)
        {
            var pageUsers =  await _userService.GetUserAllAsync(pageNumber, pageSize);
            return Ok(pageUsers);
        } 
        [HttpGet("filter-user")]
        public async Task<IActionResult> FilterUser([FromQuery] string? Fullname, [FromQuery] string? Email, [FromQuery] string? Phone,int pageNumber = 1, int pageSize = 10)
        {
            var filterUserDto = new FilterUserDto
            {
                Fullname = Fullname,
                Email = Email,
                Phone = Phone
            };
            var pageUsers =  await _userService.FilterByUserAsync(filterUserDto,pageNumber, pageSize);
            return Ok(pageUsers);
        } 
        [HttpGet("user")]
        [Authorize(Roles = "user")]
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
        [HttpPut("hidden")]
        public async Task<IActionResult> UpdateHiddenUser(int id)
        {
            int isAction = 1;
            await _userService.UpdateIsActionUserAsync(id,isAction);
            return Ok();
        } 
        [HttpPut("visible")]
        public async Task<IActionResult> UpdateVisibleUser(int id)
        {
            int isAction = 0;
            await _userService.UpdateIsActionUserAsync(id,isAction);
            return Ok();
        } 
        
    }

}