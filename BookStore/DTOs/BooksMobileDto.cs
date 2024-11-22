using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class BooksMobileDto
    {
        public int BookId {get ; set; }
        public string Title { get; set; }
        
        public string? Image { get; set; }

        public decimal? Rating { get; set; }

    }
}