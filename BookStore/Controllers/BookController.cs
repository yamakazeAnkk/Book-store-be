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
      
        public async Task<IActionResult> AddBook(
            [FromForm] CreateBookDto bookDto,
            [FromForm] UploadFilesDto filesDto)
        {
          
            if (bookDto == null || string.IsNullOrWhiteSpace(bookDto.Title) || bookDto.brandId == null || !bookDto.brandId.Any())
            {
                return BadRequest(new { message = "Invalid data for BookDto", errors = new List<string> { "Title and BrandId are required" } });
            }

            try
            {
         
                var createdBook = await _bookService.AddBookAsync(bookDto, filesDto);
                return CreatedAtAction(nameof(GetBookById), new { id = createdBook.BookId }, createdBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

      
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockBooks(int page = 1, int size = 10)
        {
            var books = await _bookService.GetLowStockBooksAsync(page, size);
            if (books.Items == null || !books.Items.Any())
            {
                return NotFound(new { message = "No low stock books found." });
            }

            return Ok(books);
        }

        [HttpGet("stagnant")]
        public async Task<IActionResult> GetStagnantBooks(int page = 1, int size = 10)
        {
            var books = await _bookService.GetStagnantBooksAsync(page, size);
            if (books.Items == null || !books.Items.Any())
            {
                return NotFound(new { message = "No stagnant books found." });
            }

            return Ok(books);
        }


        // Cập nhật sách
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(
            int id,
            [FromForm] UploadFilesDto files,
            [FromForm] CreateBookDto bookDto)
        {
          
            if (bookDto == null || string.IsNullOrWhiteSpace(bookDto.Title) || bookDto.brandId == null || !bookDto.brandId.Any())
            {
                return BadRequest(new { message = "Invalid data for BookDto", errors = new List<string> { "Title and BrandId are required" } });
            }

           try
            {
                await _bookService.UpdateBookAsync(id, bookDto, files);
                return Ok(new { message = "Book updated successfully" });
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

        [HttpPost("check-stock")]
        public async Task<IActionResult> CheckStockAndNotify()
        {
            await _bookService.CheckBookQuantityAndNotifyAsync();
            return Ok(new { message = "Notification check completed" });
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