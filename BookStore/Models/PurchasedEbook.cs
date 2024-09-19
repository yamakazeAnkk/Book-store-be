using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class PurchasedEbook
    {
        public int PurchasedEbookId { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public DateTime PurchaseDate { get; set; }

        public virtual Book? Book { get; set; }
        public virtual User? User { get; set; }
    }
}
