using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string TypeBook { get; set; } = null!;

        // Thuộc tính cho hình ảnh được upload
        public IFormFile? Image { get; set; }
    }
}