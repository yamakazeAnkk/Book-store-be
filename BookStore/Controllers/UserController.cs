using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.DTOs;
using BookStore.Services;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public async Task<IActionResult> GetAllUserPaged(int pageNumber = 1, int pageSize = 10)
        {
            var pageUsers =  await _userService.GetUserAllAsync(pageNumber, pageSize);
            return Ok(pageUsers);
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