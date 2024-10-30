using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly BookStoreContext _context;

        public VoucherRepository(BookStoreContext bookStoreContext){
            _context = bookStoreContext;
        }
        public async Task AddVoucherAsync(Voucher voucher)
        {
            await _context.Vouchers.AddAsync(voucher);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVoucherAsync(int voucherId)
        {
            var voucher = await GetVoucherByIdAsync(voucherId);

            if(voucher != null){
                _context.Vouchers.Remove(voucher);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Voucher>> GetAllVoucherAsync()
        {
            return await _context.Vouchers.ToListAsync(); 
        }

        public async Task<Voucher> GetVoucherByCodeAsync(string voucherCode)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(v => v.VoucherCode == voucherCode);
        }

        public async Task<Voucher> GetVoucherByIdAsync(int? voucherId)
        {
            return await _context.Vouchers.FindAsync(voucherId);
        }

        

        public async Task UpdateVoucherAsync(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
            await _context.SaveChangesAsync();
        }
    }
}