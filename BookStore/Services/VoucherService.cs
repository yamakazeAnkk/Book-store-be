using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.DTOs;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using BookStore.Services.Interfaces;

namespace BookStore.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;

        private readonly IMapper _mapper;

        private readonly IVoucherUserRepository _voucherUserRepository;

        public VoucherService(IVoucherUserRepository voucherUserRepository,IMapper mapper,IVoucherRepository voucherRepository){
            _voucherRepository = voucherRepository;
            _voucherUserRepository = voucherUserRepository;
            _mapper = mapper;
        }
        private string GenerateRandomVoucherCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public async Task ClaimVoucherAsync(int userId, string voucherCode)
        {
            var voucher  = await _voucherRepository.GetVoucherByCodeAsync(voucherCode);
            if(voucher == null){
                throw new Exception("Voucher không tồn tại.");
            }
            if (voucher.ExpiredDate < DateTime.UtcNow)
            {
                throw new Exception("Voucher đã hết hạn.");
            }

           
            var existingVoucherUser = await _voucherUserRepository.GetVoucherByUserIdAndVoucherIdAsync(userId, voucher.VoucherId);
            if (existingVoucherUser != null)
            {
                throw new Exception("Bạn đã nhận voucher này rồi.");
            }
            var voucherUser = new VoucherUser
            {
                UserId = userId,
                VoucherId = voucher.VoucherId,
                IsUsed = 1 
            };
            
            voucher.Quantity = (byte?)Math.Max(0, voucher.Quantity.GetValueOrDefault() - 1);
            await _voucherUserRepository.AddVoucherCodeAsync(voucherUser);
        }

        public async Task<Voucher> AddVoucherAsync(CreateVoucherDto createVoucherDto)
        {
            var voucherCode = GenerateRandomVoucherCode();

            
            var voucher = new Voucher
            {
                VoucherCode = voucherCode,
                ReleaseDate = createVoucherDto.ReleaseDate,
                ExpiredDate = createVoucherDto.ExpiredDate,
                MinCost = createVoucherDto.MinCost,
                Discount = createVoucherDto.Discount
            };

            await _voucherRepository.AddVoucherAsync(voucher);
            return voucher;
        }

        public async Task<Voucher> UpdateVoucherAsync(int voucherId, CreateVoucherDto createVoucherDto)
        {
            var voucher = await GetVoucherByIdAsync(voucherId);
            if (voucher == null){
                throw new Exception("Voucher không tồn tại.");
            }
            _mapper.Map(createVoucherDto,voucher);
            await _voucherRepository.UpdateVoucherAsync(voucher);
            return voucher;

        }

        public async Task<Voucher> GetVoucherByIdAsync(int voucherId)
        {
            return await _voucherRepository.GetVoucherByIdAsync(voucherId)
                   ?? throw new Exception("Voucher không tồn tại.");
        }

        public async Task<IEnumerable<Voucher>> GetAllVouchersAsync()
        {
            return await _voucherRepository.GetAllVoucherAsync();
        }

        public async Task DeleteVoucherAsync(int voucherId)
        {
            var voucher = await _voucherRepository.GetVoucherByIdAsync(voucherId);
            if (voucher == null)
                throw new Exception("Voucher không tồn tại.");

            await _voucherRepository.DeleteVoucherAsync(voucherId);
        }

        
    }
}