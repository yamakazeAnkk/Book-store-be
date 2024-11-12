using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookStore.DTOs
{
    public class PurchasedBookDto
    {
        public int BookId {get ; set; }
        public string Title { get; set; }
        
        public decimal Price { get; set; }

        public string? Ebook { get ; set; }

        public decimal? Rating { get; set; }

        public string? Image { get; set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; set; }
    }
}