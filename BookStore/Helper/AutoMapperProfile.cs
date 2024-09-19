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
            CreateMap<Book,BookDto>().ReverseMap();

            CreateMap<Book,CreateBookDto>().ReverseMap();
        }
    }
}