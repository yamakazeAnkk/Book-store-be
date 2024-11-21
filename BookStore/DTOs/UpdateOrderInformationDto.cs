using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class UpdateOrderInformationDto
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}