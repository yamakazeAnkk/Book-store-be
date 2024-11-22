using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class TypeBook
    {
        public TypeBook()
        {
            Books = new HashSet<Book>();
        }

        public int TypeBookId { get; set; }
        public string? TypeBookName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
