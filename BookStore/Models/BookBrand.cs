using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class BookBrand
    {
        public int BookBrandId { get; set; }
        public int? BookId { get; set; }
        public int? BandId { get; set; }

        public virtual Brand? Band { get; set; }
        public virtual Book? Book { get; set; }
    }
}
