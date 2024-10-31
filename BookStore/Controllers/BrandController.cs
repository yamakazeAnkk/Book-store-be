using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandServer;
        public BrandController(IBrandService brandServer){
            _brandServer = brandServer;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBrand(){
            var brand = await _brandServer.GetAllBrandAsync();
            return Ok(brand);
        }
        [HttpPost]
        public async Task<IActionResult> AddBrand(CreateBrandDto createBrandDto){
            try
            {
                await _brandServer.AddBrandAsync(createBrandDto);
                return Ok();
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand(int brandId,CreateBrandDto createBrandDto ){
             try
            {
                await _brandServer.UpdateBrandAsync(brandId,createBrandDto);
                return Ok(new { message = "Brand updated successfully" });
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
        public async Task<IActionResult> DeleteBrand(int id){
            try{
                await _brandServer.DeleteBrandAsync(id);
                return Ok(new { message = "Item deleted from Brand"});
            }catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

         
    }
}