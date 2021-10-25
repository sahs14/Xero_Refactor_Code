using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services
{
    public class ProductOptionService : IProductOptionService
    {
         private readonly IProductOptionRepository _productOptionRepository;
        public ProductOptionService(IProductOptionRepository productOptionRepository)
        {
            _productOptionRepository = productOptionRepository;
        }

        public async Task<ProductOption> GetProductOptionById(int productId, int id){
            return await _productOptionRepository.GetProductOptionById(productId, id);
        }
        public async Task<IEnumerable<ProductOption>> GetProductOptions(int productId){
             return await _productOptionRepository.GetProductOptions(productId);
        }

        
        public async Task CreateProductOption(int productId, ProductOptionDto productOptionDto)
        {
            await _productOptionRepository.CreateProductOption( productId,  productOptionDto);
        }

        public async Task UpdateProductOption(int id, int productId, ProductOptionDto productOptionDto){
            await _productOptionRepository.UpdateProductOption(id, productId, productOptionDto);

        }
        public async Task DeleteProductOption(int id){
            await _productOptionRepository.DeleteProductOption(id);

        }
        
    }
}