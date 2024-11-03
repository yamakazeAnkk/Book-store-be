using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookStore.DTOs
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        
        public List<int> brandId { get; set; }
        
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int? TypeBookId { get; set; }

        public string? Description { get; set; }

        public string? Ebook { get ; set; }

        public string? Image { get; set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; set; }
    }
}