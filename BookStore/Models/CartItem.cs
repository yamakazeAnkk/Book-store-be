using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class CartItem
    {
        public int CartItemId { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public int Quantity { get; set; }

        public virtual Book? Book { get; set; }
        public virtual User? User { get; set; }
    }
}
