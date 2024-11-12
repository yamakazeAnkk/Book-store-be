using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface IVoucherRepository
    {
        Task<Voucher> GetVoucherByCodeAsync(string voucherCode);
        Task<Voucher> GetVoucherByIdAsync(int? voucherId);

        Task AddVoucherAsync(Voucher voucher);

        Task <IEnumerable<Voucher>> GetAllVoucherAsync();
        Task <IEnumerable<Voucher>> GetAllVoucherOfUserAsync();

        Task UpdateVoucherAsync(Voucher voucher);
        Task DeleteVoucherAsync(int voucherId);

        Task<IEnumerable<Voucher>> GetVouchersByUserIdAsync(int userId);
    }
}