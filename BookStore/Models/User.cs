using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class User
    {
        public User()
        {
            CartItems = new HashSet<CartItem>();
            Orders = new HashSet<Order>();
            PurchasedEbooks = new HashSet<PurchasedEbook>();
            Reviews = new HashSet<Review>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Fullname { get; set; }
        public string? Address { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PurchasedEbook> PurchasedEbooks { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
