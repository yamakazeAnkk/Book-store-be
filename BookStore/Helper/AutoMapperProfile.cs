using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.DTOs;
using BookStore.Models;

namespace BookStore.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(){
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<LoginDto, User>().ReverseMap();
            // CreateMap<Book,BookDto>().ReverseMap()
            //     .ForMember(dest => dest.BookBrands, opt => opt.MapFrom(src => src.brandId.FirstOrDefault()))
            //     .ReverseMap();
            CreateMap<BookDto, Book>()
                .ForMember(dest => dest.BookBrands, opt => opt.MapFrom(src => 
                src.brandId.Distinct().Select(id => new BookBrand { BandId = id }).ToList())); // Loại bỏ các brandId trùng lặp

            // Nếu bạn cần ánh xạ ngược, tạo một ánh xạ riêng thay vì dùng ReverseMap
            CreateMap<Book, BookDto>()
            .ForMember(dest => dest.brandId, opt => opt.MapFrom(src => 
                src.BookBrands.Select(bb => bb.BandId).Distinct().ToList())); // Sử dụng Distinct để loại bỏ trùng lặp

            


            CreateMap<Book,CreateBookDto>().ReverseMap();

            CreateMap<CartItem,CartItemDto>().ReverseMap();


            CreateMap<Book,BookDetailsDto>().ForMember(dest => dest.BrandNames, opt => opt.MapFrom(src => src.BookBrands.Select(bb => bb.Band != null ? bb.Band.Name : string.Empty).ToList())).ReverseMap();

            CreateMap<CartItem,CartItemDto>()
                .ForMember(dest =>dest.quantity , opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.total, otp => otp.MapFrom(src => src.Quantity * src.Book.Price) )
                .ForMember(dest => dest.bookDto , otp => otp.MapFrom(src => src.Book)).ReverseMap();
            
            CreateMap<Order,OrderDto>().ForMember(dest => dest.OrderItems,opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<OrderItem,OrderItemDto>().ReverseMap();
            CreateMap<Order,CreateOrderDto>().ReverseMap();
            CreateMap<ProductCount, OrderItemDto>()
            .ForMember(dest => dest.BookDto, opt => opt.MapFrom(src => src.Book)); 

            CreateMap<Review, ReviewDto>()
                .ReverseMap(); 

            CreateMap<Review, ReviewDetailDto>()
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User != null ? src.User.Fullname : "Anonymous"))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User != null ? src.User.Email : "N/A"))
            .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.User != null ? src.User.ProfileImage : "default-avatar.png"))
            .ReverseMap();

                
            CreateMap<CreateReviewDto, Review>()
                .ReverseMap(); 
            CreateMap<CreateUserDetailDto,User>().ReverseMap();
            CreateMap<UserDetailDto,User>().ReverseMap();

            CreateMap<BrandDto,Brand>().ReverseMap();

            CreateMap<CreateVoucherDto,Voucher>().ReverseMap();
            CreateMap<VoucherDetailDto,Voucher>().ReverseMap();

        }       
    }
}