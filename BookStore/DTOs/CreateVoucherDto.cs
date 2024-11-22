using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class CreateVoucherDto
    {
        
        
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public decimal MinCost { get; set; }

        public int? Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}