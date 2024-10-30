using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            VoucherUsers = new HashSet<VoucherUser>();
        }

        public int VoucherId { get; set; }
        public string VoucherCode { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public decimal MinCost { get; set; }
        public decimal Discount { get; set; }

        public virtual ICollection<VoucherUser> VoucherUsers { get; set; }
    }
}
