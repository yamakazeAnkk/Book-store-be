using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public List<int> brandId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int? TypeBookId { get; set; }

        public string? Image { get; set; }
    }
}