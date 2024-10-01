using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Book
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
            BookBrands = new HashSet<BookBrand>();
            CartItems = new HashSet<CartItem>();
            OrderItems = new HashSet<OrderItem>();
            PurchasedEbooks = new HashSet<PurchasedEbook>();
            Reviews = new HashSet<Review>();
        }

        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string? Image { get; set; }
        public int? TypeBookId { get; set; }
        public DateTime UploadDate { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public decimal? Rating { get; set; }

        public virtual TypeBook? TypeBook { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<BookBrand> BookBrands { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<PurchasedEbook> PurchasedEbooks { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
