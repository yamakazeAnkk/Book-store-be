using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Helper
{
    public class PaginatedResult <T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public PaginatedResult(IEnumerable<T> items, int totalCount, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize); // Cách tính đúng
        }
    }
}