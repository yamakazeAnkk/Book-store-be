using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DTOs
{
    public class CartItemDto
    {
        public int quantity {get ; set;}

        public decimal total { get ; set ;}

        public BookDto bookDto {get ; set ;}

    }
}