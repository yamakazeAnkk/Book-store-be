using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Author
    {
        public Author()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        public int AuthorId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
