using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Brand
    {
        public Brand()
        {
            BookBrands = new HashSet<BookBrand>();
        }

        public int BrandId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<BookBrand> BookBrands { get; set; }
    }
}
