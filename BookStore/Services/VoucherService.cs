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

        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        private readonly IVoucherUserRepository _voucherUserRepository;

        public VoucherService(IUserRepository userRepository,IVoucherUserRepository voucherUserRepository,IMapper mapper,IVoucherRepository voucherRepository){
            _voucherRepository = voucherRepository;
            _voucherUserRepository = voucherUserRepository;
            _mapper = mapper;
            _userRepository = userRepository;
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

           
            var existingVoucherUser = await _voucherUserRepository.GetVoucherByUserAndVoucherIdAsync(userId, voucher.VoucherId);
            if (existingVoucherUser != null)
            {
                throw new Exception("Bạn đã nhận voucher này rồi.");
            }
            var voucherUser = new VoucherUser
            {
                UserId = userId,
                VoucherId = voucher.VoucherId,
                IsUsed = 0 
            };
            
            voucher.Quantity = (byte?)Math.Max(0, voucher.Quantity.GetValueOrDefault() - 1);
            await _voucherUserRepository.AddVoucherCodeAsync(voucherUser);
        }

        public async Task AddVoucherAsync(CreateVoucherDto createVoucherDto)
        {
            var voucherCode = GenerateRandomVoucherCode();

            
            var voucher = new Voucher
            {
                VoucherCode = voucherCode,
                ReleaseDate = createVoucherDto.ReleaseDate,
                ExpiredDate = createVoucherDto.ExpiredDate,
                MinCost = createVoucherDto.MinCost,
                Quantity = createVoucherDto.Quantity,
                Discount = createVoucherDto.Discount
            };

            await _voucherRepository.AddVoucherAsync(voucher);
            
        }

        public async Task UpdateVoucherAsync(int voucherId, CreateVoucherDto createVoucherDto)
        {
            var voucher = await _voucherRepository.GetVoucherByIdAsync(voucherId);
            if (voucher == null){
                throw new Exception("Voucher không tồn tại.");
            }
            _mapper.Map(createVoucherDto,voucher);
            
            await _voucherRepository.UpdateVoucherAsync(voucher);
            

        }

        public async Task<VoucherDto> GetVoucherByIdAsync(int voucherId)
        {
            var voucher = await _voucherRepository.GetVoucherByIdAsync(voucherId)
                   ?? throw new Exception("Voucher không tồn tại.");
            return _mapper.Map<VoucherDto>(voucher);
        }

        public async Task<IEnumerable<VoucherDto>> GetAllVouchersAsync()
        {
            var voucher =  await _voucherRepository.GetAllVoucherAsync();
            return _mapper.Map<IEnumerable<VoucherDto>>(voucher);
        }

        public async Task DeleteVoucherAsync(int voucherId)
        {
            var voucher = await _voucherRepository.GetVoucherByIdAsync(voucherId);
            if (voucher == null)
                throw new Exception("Voucher không tồn tại.");

            await _voucherRepository.DeleteVoucherAsync(voucherId);
        }

        public async Task<IEnumerable<VoucherDto>> GetAllVouchersByUserAsync(string username)
        {
            var user = await _userRepository.GetUserByEmailAsync(username);
            if(user == null){
                throw new Exception("User không tồn tại.");
            }
            var voucher = await _voucherRepository.GetVouchersByUserIdAsync(user.UserId);
            return _mapper.Map<IEnumerable<VoucherDto>>(voucher);
            
        }

        public async Task<IEnumerable<VoucherDto>> GetAllVouchersOfUserAsync(string username)
        {
            var user = await _userRepository.GetUserByEmailAsync(username);
            if(user == null){
                throw new Exception("User không tồn tại.");
            }
            var voucher =  await _voucherRepository.GetAllVoucherOfUserAsync(user.UserId);
            return _mapper.Map<IEnumerable<VoucherDto>>(voucher);
        }
    }
}