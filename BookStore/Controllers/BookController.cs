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
using Newtonsoft.Json;

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
        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> GetBooksPaged(int pageNumber = 1, int pageSize = 10)
        {
            var pagedBooks = await _bookService.GetBooksPagedAsync(pageNumber, pageSize);
            return Ok(pagedBooks);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks(string name ,int pageNumber=  1 ,int pageSize =10 ){
            var pagedBooks = await _bookService.SearchBooksByTitleAsync(name,pageNumber,pageSize);
            return Ok(pagedBooks);
        }
        [HttpGet("brand")]
        public async Task<IActionResult> SearchBooksByBrand([FromQuery]List<int> id ,int pageNumber=  1 ,int pageSize =10 ){
            var pagedBooks = await _bookService.FilterBooksByBrandAsync(id,pageNumber,pageSize);
            return Ok(pagedBooks);
        }
        [HttpGet("byPriceRange")]
        public async Task<IActionResult> SearchPriceRange(decimal min,decimal max ,int pageNumber=  1 ,int pageSize =10 ){
            var pagedBooks = await _bookService.SortBooksByPriceAsync(min,max,pageNumber,pageSize);
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

      
        [HttpPost("create")]
        public async Task<IActionResult> AddBookWithImage(
            [FromForm] IFormFile imageFile,   
            [FromForm(Name = "BookJson")] string bookJson)     
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

          
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Please select an image file.");
            }
                BookDto bookDto;
                try
                {
                    bookDto = JsonConvert.DeserializeObject<BookDto>(bookJson);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = "Invalid JSON format for BookDto", error = ex.Message });
                }

               
                if (bookDto == null || string.IsNullOrWhiteSpace(bookDto.Title) || bookDto.brandId == null || !bookDto.brandId.Any())
                {
                    return BadRequest(new { message = "Invalid data for BookDto", errors = new List<string> { "Title and brandId are required" } });
                }
            try
            {
              
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var contentType = imageFile.ContentType;

                using (var stream = imageFile.OpenReadStream())
                {
                  
                    var fileUrl = await _firebaseStorageService.UploadImageAsync(stream, fileName, contentType);

                  
                   bookDto.Image = fileUrl; 
                }

      
                var createdBook = await _bookService.AddBookAsync(bookDto);

                return Ok(new { message = "Book created successfully", book = createdBook });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

      
        


        // Cập nhật sách
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(
            int id,
            [FromForm] IFormFile imageFile,
            [FromForm(Name = "BookJson")] string bookJson)
        {
            // Validate input data
            if (string.IsNullOrWhiteSpace(bookJson))
            {
                return BadRequest("Invalid BookJson data.");
            }

            BookDto bookDto;
            try
            {
                // Deserialize the JSON string into a BookDto object
                bookDto = JsonConvert.DeserializeObject<BookDto>(bookJson);
            }
            catch (JsonException ex)
            {
                return BadRequest(new { message = "Invalid JSON format for BookDto", error = ex.Message });
            }

            // Validate required fields in BookDto
            if (bookDto == null || string.IsNullOrWhiteSpace(bookDto.Title) || bookDto.brandId == null || !bookDto.brandId.Any())
            {
                return BadRequest(new { message = "Invalid data for BookDto", errors = new List<string> { "Title and BrandId are required" } });
            }

            try
            {
                // Handle the image file upload if a new image is provided
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Check if the uploaded file is an image
                    if (!imageFile.ContentType.StartsWith("image/"))
                    {
                        return BadRequest("Only image files are allowed.");
                    }

                    // Generate a unique filename
                    var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    var contentType = imageFile.ContentType;

                    // Upload the image to Firebase or your preferred storage service
                    using (var stream = imageFile.OpenReadStream())
                    {
                        var fileUrl = await _firebaseStorageService.UploadImageAsync(stream, fileName, contentType);
                        
                        // Update the Image URL in the BookDto
                        bookDto.Image = fileUrl;
                    }
                }

                // Update the book details using your service layer
                await _bookService.UpdateBookAsync(id, bookDto);

                return Ok(new { message = "Book updated successfully", book = bookDto });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
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