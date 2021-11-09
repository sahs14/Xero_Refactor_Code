using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Errors;
using API.Interfaces;
using API.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService>  logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Product> GetProductById(int id){
            return await _productRepository.GetProductById(id);
        }

        public async Task<IEnumerable<Product>> GetProducts() {
            return await _productRepository.GetProducts();
        }

        public async Task<ManageResult> CreateProduct(ProductDto productDto)
        {
            var errorMessage = productDto.Validate();
             if(errorMessage.Count > 0)
             {
                 _logger.LogError("Product validation failed");
                 return new ManageResult(false, ErrorType.BadRequest, errorMessage);
             }
            var productList = await _productRepository.GetProducts();
            foreach (var item in productList)
            {
                if(item.Name == productDto.Name){
                    return new ManageResult(false, ErrorType.BadRequest, "Product name already exist");
                }
               
            }
            await _productRepository.CreateProduct(productDto);
            return new ManageResult(true, ErrorType.Ok,"Product Created");
        }
        
        public async Task DeleteProduct(int id) {
            await _productRepository.DeleteProduct(id);

        }

        public async Task UpdateProduct(int id, ProductDto productDto)
        {
            await _productRepository.UpdateProduct(id, productDto);
        }

        public async Task<ManageResult> ModifyProduct(int id, ProductDto productDto)
        {
             var errorMessage = productDto.Validate();
             if(errorMessage.Count > 0)
             {
                 _logger.LogError("Product validation failed" + id);
                 return new ManageResult(false, ErrorType.BadRequest, errorMessage);
             }
             if (id == default(int))
             {
                return new ManageResult(false, ErrorType.NotFound , "Product Id invalid");
             }

             var productInfo = await _productRepository.GetProductById(id);
             if (productInfo == null)
             {
                return new ManageResult(false, ErrorType.NotFound, "Product not found");
             }
             else{
                 await _productRepository.UpdateProduct(id, productDto);
                 return new ManageResult(true, ErrorType.Ok , "Product updated");
             }
        }

        public async Task<Product> GetProductByProductOption(int id){
            return await _productRepository.GetProductByProductOption(id);
        }
    }
}