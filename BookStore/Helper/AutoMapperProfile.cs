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
            CreateMap<Book,BookDto>().ReverseMap().ForMember(dest => dest.BookBrands, opt => opt.MapFrom(src => src.brandId.FirstOrDefault())).ReverseMap();

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
            .ForMember(dest => dest.BookDto, opt => opt.MapFrom(src => src.Book)); // Map Book to BookDto
        }   
    }
}