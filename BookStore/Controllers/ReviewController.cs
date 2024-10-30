using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "user")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDetailDto>> GetReview(int id)
        {
            var reviewDto = await _reviewService.FindAllByProductAsync(id);

            return Ok(reviewDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReview([FromForm] CreateReviewDto createReviewDto)
        {
            var emailUser = User.FindFirst(ClaimTypes.Email)?.Value;
                if (string.IsNullOrEmpty(emailUser))
                    return Unauthorized("User not authenticated");

            await _reviewService.CreateAsync(createReviewDto,emailUser);
            
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] CreateReviewDto createReviewDto)
        {
            await _reviewService.UpdateAsync(id, createReviewDto);
            return NoContent();
        }
        
    }
}