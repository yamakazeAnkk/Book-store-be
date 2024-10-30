using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class ReviewDto
    {
        public int ReviewId  { get; set; }

        public string? Comment {get ; set ;}

        public int BookId  { get; set; }

        public int UserId  { get; set; }

        public int Rating  { get; set; }
    }
}