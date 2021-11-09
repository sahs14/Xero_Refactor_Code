using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Errors;
using API.Interfaces;
using API.Validators;
using Microsoft.Extensions.Logging;

namespace API.Services
{
    public class ProductOptionService : IProductOptionService
    {
         private readonly IProductOptionRepository _productOptionRepository;

         private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductOptionService> _logger;
        public ProductOptionService(IProductOptionRepository productOptionRepository, IProductRepository productRepository, ILogger<ProductOptionService>  logger)
        {
            _productOptionRepository = productOptionRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<ProductOption> GetProductOptionById(int productId, int id){
            return await _productOptionRepository.GetProductOptionById(productId, id);
        }
        public async Task<IEnumerable<ProductOption>> GetProductOptions(int productId){
             return await _productOptionRepository.GetProductOptions(productId);
        }

        
        public async Task<ManageResult> CreateProductOption(int productId, ProductOptionDto productOptionDto)
        {
            var errorMessages = productOptionDto.Validate();

            if (errorMessages.Count > 0)
                return new ManageResult(false, ErrorType.BadRequest , errorMessages);

            var product = await _productRepository.GetProductById(productId);

            if(product == null)
            {
                return new ManageResult(false, ErrorType.NotFound,"Product does not exist");
            }

            if(product.ProductOpt?.Count() > 0 && product.ProductOpt?.Any(st => st.Name.ToLower().Trim() == productOptionDto.Name.ToLower().Trim()) == true)
            {
                return new ManageResult(false, ErrorType.BadRequest,"ProductOption with same name already exist");
            }

            await _productOptionRepository.CreateProductOption( productId,  productOptionDto);
            return new ManageResult(true, ErrorType.Ok, "ProductOption Added");
        }

        public async Task UpdateProductOption(int id, int productId, ProductOptionDto productOptionDto){
            await _productOptionRepository.UpdateProductOption(id, productId, productOptionDto);

        }
        public async Task DeleteProductOption(int id){
            await _productOptionRepository.DeleteProductOption(id);

        }
        public async Task<ManageResult> ModifyProductOption(int id, int productId, ProductOptionDto productOptionDto)
        {
            if (id == default(int) || productId == default(int))
                return new ManageResult(false, ErrorType.BadRequest , "Invalid Product Id");

            var errorMessages = productOptionDto.Validate();

            if (errorMessages.Count > 0)
                return new ManageResult(false, ErrorType.BadRequest , errorMessages);

            var productOption = await _productOptionRepository.GetProductOptionById(productId, id);

            if (productOption != null)
            {
                var product = await _productRepository.GetProductById(productId);

                if (product == null)
                {
                    return new ManageResult(false, ErrorType.NotFound,"Product does not exist");
                }

                if(product.ProductOpt?.Count() > 0 && product.ProductOpt?.Any(st => st.Name.ToLower().Trim() == productOptionDto.Name.ToLower().Trim()) == true)
                {
                    return new ManageResult(false, ErrorType.BadRequest,"ProductOption with same name already exist");
                }
            }

            if (productOption == null)
            {
                return new ManageResult(false, ErrorType.NotFound,"ProductOption not found");
            }
            else
            {
                 await _productOptionRepository.UpdateProductOption(id, productId, productOptionDto);
                return new ManageResult(true, ErrorType.Ok, "ProductOption successfully updated");
            }
        }
        
    }
}