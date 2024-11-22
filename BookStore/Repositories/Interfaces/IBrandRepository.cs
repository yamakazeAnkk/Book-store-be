using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repositories.Interfaces
{
    public interface IBrandRepository
    {
        Task<Brand> GetBrandByIdAsync(int brandId);

        // Lấy tất cả các Brands
        Task<IEnumerable<Brand>> GetAllBrandsAsync();

        // Thêm Brand mới
        Task<Brand> AddBrandAsync(Brand brand);

        // Cập nhật Brand
        Task UpdateBrandAsync(Brand brand);

        // Xóa Brand
        Task DeleteBrandAsync(int brandId);
    }
}