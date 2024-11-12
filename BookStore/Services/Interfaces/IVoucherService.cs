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

        Task AddVoucherAsync(CreateVoucherDto createVoucherDto); 
        Task UpdateVoucherAsync(int voucherId, CreateVoucherDto createVoucherDto); 
        Task<VoucherDto> GetVoucherByIdAsync(int voucherId); 
        Task<IEnumerable<VoucherDto>> GetAllVouchersAsync();
        Task<IEnumerable<VoucherDto>> GetAllVouchersByUserAsync(string username);
        Task DeleteVoucherAsync(int voucherId); 
        Task<IEnumerable<VoucherDto>> GetAllVouchersOfUserAsync();
       


    }
}