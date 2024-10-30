using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class ReviewDetailDto
    {
        public string? Comment { get; set; }
        public string? FullName  { get; set; }
        public string? Email  { get; set; }
        public string? Avatar { get; set; }
        public decimal Rating { get; set; }
    }
}