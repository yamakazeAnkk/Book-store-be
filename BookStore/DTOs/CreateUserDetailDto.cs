using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class CreateUserDetailDto
    {
       
        public string? Email { get; set; } 
        public string? Phone { get; set; }
        public string? Fullname { get; set; }
        public string? Address { get; set; }


    }
}