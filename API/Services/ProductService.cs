using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        public async Task CreateProduct(ProductDto productDto)
        {
            await _productRepository.CreateProduct(productDto);
        }
        
        public async Task DeleteProduct(int id) {
            await _productRepository.DeleteProduct(id);

        }

        public async Task UpdateProduct(int id, ProductDto productDto)
        {
            await _productRepository.UpdateProduct(id, productDto);
        }
    }
}