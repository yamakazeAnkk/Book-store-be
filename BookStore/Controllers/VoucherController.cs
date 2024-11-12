using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService){
            _voucherService =voucherService;
        }
        [HttpPost]
        public async Task<IActionResult> AddVoucher(CreateVoucherDto createVoucherDto){
            
            try
            {
                await _voucherService.AddVoucherAsync(createVoucherDto);
                return Ok();
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateVoucher(int voucherId,CreateVoucherDto createVoucherDto){
            try
            {
                await _voucherService.UpdateVoucherAsync(voucherId,createVoucherDto);
                return Ok(new { message = "Voucher updated successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucher(int id){
            try{
                await _voucherService.DeleteVoucherAsync(id);
                return Ok(new { message = "item deleted from Voucher"});
            }catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpGet("amdin")]
        public async Task<IActionResult> GetAllVoucher(){
            var voucher = await _voucherService.GetAllVouchersAsync();
            return Ok(voucher);
        }
        [HttpGet("of-mobile")]
        public async Task<IActionResult> GetAllVoucherOfUser(){
            var voucher = await _voucherService.GetAllVouchersOfUserAsync();
            return Ok(voucher);
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetAllVoucherByUser(){
            var emailUser = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(emailUser))
            {
                return Unauthorized("User not authenticated");
            }

            try
            {
                var vouchers = await _voucherService.GetAllVouchersByUserAsync(emailUser);
                return Ok(vouchers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
          
        }
    }
}