using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;

namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductById(int id){
            return await _productRepository.GetProductById(id);
        }
        public async Task<IEnumerable<Product>> GetProducts() {
            return await _productRepository.GetProducts();
        }
        
    }
}