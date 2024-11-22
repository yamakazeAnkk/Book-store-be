using System;
using System.Collections.Generic;

namespace BookStore.Models
{
    public partial class VoucherUser
    {
        public int VoucherUserId { get; set; }
        public int? VoucherId { get; set; }
        public int? UserId { get; set; }
        public int? IsUsed { get; set; }

        public virtual User? User { get; set; }
        public virtual Voucher? Voucher { get; set; }
    }
}
