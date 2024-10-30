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
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository, IMapper mapper){
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        
        public async Task<Brand> AddBrandAsync(CreateBrandDto createBrandDto)
        {
            var brand = _mapper.Map<Brand>(createBrandDto);
            
            return await _brandRepository.AddBrandAsync(brand);
        }

        public async Task DeleteBookAsync(int brandId)
        {
            var brands = await _brandRepository.GetBrandByIdAsync(brandId);
            if(brands == null){
                throw new Exception("Brand not found");
            }
           
            await _brandRepository.DeleteBrandAsync(brandId);
        }
        

        public async Task<IEnumerable<BrandDto>> GetAllBrandAsync()
        {
            var brands = await _brandRepository.GetAllBrandsAsync();
            return _mapper.Map<IEnumerable<BrandDto>>(brands);

        }

        public async Task UpdateBookAsync(int id, CreateBrandDto createBrandDto)
        {
            var brands = await _brandRepository.GetBrandByIdAsync(id);
            if(brands == null){
                throw new Exception("Brand not found");
            }
            _mapper.Map(createBrandDto,brands);
            await _brandRepository.UpdateBrandAsync(brands);
        }
    }
}