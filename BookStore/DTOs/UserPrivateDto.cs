using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class UserPrivateDto
    {
        public int UserId { get; set; }
        public string? ProfileImage { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string? Address { get; set; }
    }
}