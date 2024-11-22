using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount {get ; set;}
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public string Status { get; set; } = null!;
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}