using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class CreateReviewDto
    {
        public string? Comment  { get; set; }
        public int BookId  { get; set; }

        public int Rating  { get; set; }



    }
}