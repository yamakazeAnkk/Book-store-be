using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class UserDetailDto
    {
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Fullname { get; set; }
        public string? Address { get; set; }
        public int? RoleId { get; set; }

    
        
        

    }
}