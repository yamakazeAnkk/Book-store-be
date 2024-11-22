using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DTOs;
using BookStore.Models;

namespace BookStore.Services.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandDto>> GetAllBrandAsync();

        Task<Brand> AddBrandAsync(CreateBrandDto createBrandDto);

        Task UpdateBrandAsync(int id,CreateBrandDto createBrandDto);
        Task DeleteBrandAsync(int brandId);
    }
}