using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class BooksDto
    {
        public int BookId {get ; set; }
        public string Title { get; set; }
        
        public decimal Price { get; set; }

        public string? Image { get; set; }

        public string AuthorName { get; set; }

        public int IsSale { get; set; }

        public decimal? Rating { get; set; }

        public List<string> BrandNames { get; set; }
    }

}