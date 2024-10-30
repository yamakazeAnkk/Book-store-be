using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly BookStoreContext _bookStoreContext;

        public BrandRepository(BookStoreContext bookStoreContext){
            _bookStoreContext = bookStoreContext;
        }
        public async Task<Brand> AddBrandAsync(Brand brand)
        {
            _bookStoreContext.Add(brand);
            await _bookStoreContext.SaveChangesAsync();
            return brand;
        }

        public async Task DeleteBrandAsync(int brandId)
        {
            var brand = await _bookStoreContext.Brands.FindAsync(brandId);
            if(brand != null){
                _bookStoreContext.Brands.Remove(brand);
                await _bookStoreContext.SaveChangesAsync();
            }
            
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _bookStoreContext.Brands.ToListAsync();
        }

        public async Task<Brand> GetBrandByIdAsync(int brandId)
        {
            return await _bookStoreContext.Brands.FindAsync(brandId);
        }

        public async Task UpdateBrandAsync(Brand brand)
        {
            _bookStoreContext.Brands.Update(brand);
            await _bookStoreContext.SaveChangesAsync();
        }
    }
}