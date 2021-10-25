using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
    }
}