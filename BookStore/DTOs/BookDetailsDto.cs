using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class BookDetailsDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public DateTime UploadDate { get; set; }

        public int Quantity { get; set; }
        public int? TypeBookId { get; set; }
        public string? Image { get; set; }

        public string? LinkEbook { get; set; }

        public decimal? Rating { get; set; }
        public string? Description { get; set; }

        public string AuthorName { get; set; }

        public List<string> BrandNames { get; set; }
    }
}