using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;

namespace BookStore.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IFeedBackRepository _feedbackRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ReviewService(
            IFeedBackRepository feedBackRepository,
            IBookRepository bookRepository,
            IUserRepository userRepository,
            IMapper mapper
        ){
            _feedbackRepository = feedBackRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<long> CreateAsync(CreateReviewDto createReviewDto, string emailUser)
        {
            var user = await _userRepository.GetUserByEmailAsync(emailUser)?? throw new Exception("User not found");
            var book = await _bookRepository.GetBookByIdAsync(createReviewDto.BookId)?? throw new Exception("User not found");

            var review = _mapper.Map<Review>(createReviewDto);
            review.BookId = book.BookId;
            review.UserId = user.UserId;

            await _feedbackRepository.AddReviewAsync(review);

            var allReviews = await _feedbackRepository.GetReviewsByBookIdAsync(book.BookId);
            var averageRating = allReviews.Any() ? allReviews.Average(r => r.Rating) : review.Rating;

            book.Rating = averageRating;
            await _bookRepository.UpdateBookAsync(book);


            return review.ReviewId;
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _feedbackRepository.GetReviewByIdAsync(id);
                

     
            await _feedbackRepository.DeleteReviewAsync(id);
        }

        public async Task<List<ReviewDto>> FindAllAsync()
        {
            var review = await _feedbackRepository.FindAllAsync();
            return _mapper.Map<List<ReviewDto>>(review);
        }

        public async Task<List<ReviewDetailDto>> FindAllByProductAsync(int productId)
        {

            var book = await _bookRepository.GetBookByIdAsync(productId);
            if(book == null){
                return new List<ReviewDetailDto>();
            }

            var reviews = await _feedbackRepository.findAllByReview(book);

            if (reviews == null || !reviews.Any())
            {
                return new List<ReviewDetailDto>();
            }
            var reviewDetails = reviews.Select(review => new ReviewDetailDto{
                Comment = review.Comment,
                Username = review.User?.Username,
                // Email = review.User?.Email,
                Avatar = review.User?.ProfileImage,
                Rating = RoundToNearestHalf(review.Rating)
            }).ToList();
            return reviewDetails;
        }
        private decimal RoundToNearestHalf(decimal rating)
        {
            return Math.Round(rating * 2, MidpointRounding.AwayFromZero) / 2;
        }

        public async Task<ReviewDto> FindByIdAsync(int id)
        {
            var review = await _feedbackRepository.GetReviewByIdAsync(id);
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task UpdateAsync(int id, CreateReviewDto reviewDto)
        {
            var review = await _feedbackRepository.GetReviewByIdAsync(id);



            _mapper.Map(reviewDto, review);

            await _feedbackRepository.SaveAsync(review);
        }
    }
}