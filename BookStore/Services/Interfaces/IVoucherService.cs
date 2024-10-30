using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace BookStore.Services.Interfaces
{
    public interface IVoucherService
    {
        Task ClaimVoucherAsync(int userId, string voucherCode);

        Task<Voucher> AddVoucherAsync(CreateVoucherDto createVoucherDto); 
        Task<Voucher> UpdateVoucherAsync(int voucherId, CreateVoucherDto createVoucherDto); 
        Task<Voucher> GetVoucherByIdAsync(int voucherId); 
        Task<IEnumerable<Voucher>> GetAllVouchersAsync(); 
        Task DeleteVoucherAsync(int voucherId); 


    }
}