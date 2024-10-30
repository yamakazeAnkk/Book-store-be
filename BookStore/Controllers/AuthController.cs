using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.Services;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly JwtService _jwtService;

        private readonly FirebaseStorageService _firebaseStorageService;

        public AuthController(IUserService userService, JwtService jwtService,FirebaseStorageService firebaseStorageService){
            _userService = userService;
            _jwtService = jwtService;
            _firebaseStorageService = firebaseStorageService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(
            [FromForm] IFormFile imageFile, 
            [FromForm(Name="UserJson")]string userJson)
        {
            // Validate the incoming UserDto
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserDto userDto;
            userDto = JsonConvert.DeserializeObject<UserDto>(userJson);
            // Check if required fields in UserDto are present
            if (userDto == null || string.IsNullOrWhiteSpace(userDto.Username) ||
                string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.Password))
            {
                return BadRequest(new { message = "Username, Email, and Password are required." });
            }

            try
            {
                // Handle the image upload if an image file is provided
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Validate that the uploaded file is an image
                    if (!imageFile.ContentType.StartsWith("image/"))
                    {
                        return BadRequest("Only image files are allowed.");
                    }

                    // Generate a unique filename
                    var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    var contentType = imageFile.ContentType;

                    // Upload the image file to your storage service
                    using (var stream = imageFile.OpenReadStream())
                    {
                        var fileUrl = await _firebaseStorageService.UploadImageAsync(stream, fileName, contentType);

                        // Set the ProfileImage URL in the UserDto
                        userDto.ProfileImage = fileUrl;
                    }
                }

                // Register the user using your user service
                var registeredUser = await _userService.RegisterUserAsync(userDto);

                return Ok(new { message = "User registered successfully", user = registeredUser });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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