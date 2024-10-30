using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class OrderItemDto
    {
        public int BookId {get ; set ;}
       
        public int Quantity { get; set; }

        public int Count {get ; set ;}

        public BookDto BookDto {get ; set ;}

        
    }
}