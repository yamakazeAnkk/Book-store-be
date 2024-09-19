using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.Services;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        
        private readonly FirebaseStorageService _firebaseStorageService;
        public BookController(IBookService bookService, FirebaseStorageService firebaseStorageService){
            _firebaseStorageService = firebaseStorageService;
            _bookService = bookService;
        }
        // Phân trang
        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetBooksPaged(int pageNumber = 1, int pageSize = 10)
        {
            var pagedBooks = await _bookService.GetBooksPagedAsync(pageNumber, pageSize);
            return Ok(pagedBooks);
        }

        // Lấy sách theo ID
        [HttpGet("{id}")]
       
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // Thêm sách
        [HttpPost]
        public async Task<IActionResult> AddBook([FromForm] CreateBookDto bookDto)
        {
            string? imageUrl = null;
            if(bookDto.Image != null && bookDto.Image.Length > 0){
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(bookDto.Image.FileName);
                var contentType = bookDto.Image.ContentType;

                using (var stream = bookDto.Image.OpenReadStream())
                {
                    imageUrl = await _firebaseStorageService.UploadImageAsync(stream,fileName,contentType);
                }
            }

            await _bookService.AddBookAsync(bookDto,imageUrl);
            return Ok("Book added successfully.");
        }

        // Cập nhật sách
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto bookDto)
        {
           
            try
            {
                await _bookService.UpdateBookAsync(id,bookDto);
                return Ok("Book update successfully");
            }
            catch (ArgumentException ex)
            {
                
                return NotFound(ex.Message );
            }
          
        }

        // Xóa sách
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return Ok("Book deleted successfully.");
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0){
                return BadRequest("Please select an image file.");
            }
                

           try
            {
                
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var contentType = imageFile.ContentType;

           
                using (var stream = imageFile.OpenReadStream())
                {
                    
                    var fileUrl = await _firebaseStorageService.UploadImageAsync(stream, fileName, contentType);

                 
                    return Ok(new { FileUrl = fileUrl });
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Lỗi khi tải file lên: {ex.Message}");
            }
        }


    }
}