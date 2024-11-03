using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class VoucherUserRepository : IVoucherUserRepository
    {
        private readonly BookStoreContext _context;
        public VoucherUserRepository(BookStoreContext bookStoreContext){
            _context = bookStoreContext;
        }

        public async Task AddVoucherCodeAsync(VoucherUser voucherUser)
        {
            await _context.VoucherUsers.AddAsync(voucherUser);
            await _context.SaveChangesAsync();
        }

        public async Task<VoucherUser> GetUnusedVoucherByUserIdAsync(int userId)
        {
           return await _context.VoucherUsers
            .FirstOrDefaultAsync(vu => vu.UserId == userId);
        }

   

        public async Task<VoucherUser> GetVoucherByUserAndVoucherIdAsync(int userId,int voucherId)
        {
            return await _context.VoucherUsers
            .FirstOrDefaultAsync(vu => vu.UserId == userId && vu.VoucherId == voucherId);
        }

        public async Task UpdateVoucherUserAsync(VoucherUser voucherUser)
        {
            _context.VoucherUsers.Update(voucherUser);
            await _context.SaveChangesAsync();
        }
    }
}