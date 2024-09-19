using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class UserDto
    {
        public string Username { get ; set ; } = null!;

        public string Password {  get ; set ;  } = null!;
        
        public string Email { get ; set ;} = null!;


    }
}