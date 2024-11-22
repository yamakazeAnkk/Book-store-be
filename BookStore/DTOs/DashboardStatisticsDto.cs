using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class DashboardStatisticsDto
    {
        public int BookCount { get; set; }
        public int OrderCount { get; set; }
        public int UserCount { get; set; }
        public decimal TotalOrderAmount { get; set; }

        public int ReviewCount { get; set; }
    }
}