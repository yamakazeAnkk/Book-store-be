using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface IVoucherUserRepository
    {
        Task<VoucherUser> GetVoucherByUserIdAndVoucherIdAsync(int userId, int voucherId);
        Task UpdateVoucherUserAsync(VoucherUser voucherUser);

        Task AddVoucherCodeAsync(VoucherUser voucherUser);

        Task<VoucherUser> GetUnusedVoucherByUserIdAsync(int userId);
    } 
}