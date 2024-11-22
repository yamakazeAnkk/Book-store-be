using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class OrderItemDto
    {
        
       
        public int Quantity { get; set; }
        public BooksDto BooksDto {get ; set ;}

        
    }
}