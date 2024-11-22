using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class FilterOrderDto
    {
        public string? Phone { get; set; }

        public string? Status { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }
    }
}