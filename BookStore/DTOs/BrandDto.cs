using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class BrandDto
    {
        public int BrandId { get; set; }
        public string Name { get; set; } = null!;
    }
}