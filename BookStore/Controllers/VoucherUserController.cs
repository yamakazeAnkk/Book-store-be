using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "user")]
    public class VoucherUserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IVoucherService _voucherService;


        public VoucherUserController(IUserService userService, IVoucherService voucherService){
            _userService = userService;
            _voucherService = voucherService;
        }
        [HttpPost("code")]
        public async Task<IActionResult> ClaimVoucher([FromForm] string voucherCode){

            try
            {
                var emailUser = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
                if (string.IsNullOrEmpty(emailUser))
                    return Unauthorized(new { message = "Người dùng chưa đăng nhập" });

                var user = await _userService.GetUserByEmailAsync(emailUser);
                if (user == null)
                    return NotFound(new { message = "Không tìm thấy người dùng" });

                await _voucherService.ClaimVoucherAsync(user.UserId, voucherCode);

                return Ok(new { message = "Voucher đã được thêm thành công vào tài khoản của bạn." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        

        
    }
}