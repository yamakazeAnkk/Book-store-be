using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.Services;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly JwtService _jwtService;

        public AuthController(IUserService userService, JwtService jwtService){
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserDto request){
            try
            {
                var user = await _userService.RegisterUserAsync(request);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.LoginUserAsync(loginDto);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            if(user.Role == null || string.IsNullOrEmpty(user.Role.RoleName)){
                return Unauthorized("User role is not defined.");
            }
            var token = _jwtService.GenerateToken(user.UserId.ToString(), user.Role.RoleName,user.Email);
            return Ok(new { Token = token , Username = user.Username, Role = user.Role.RoleName });
        }
    }
}