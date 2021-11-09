using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProducts();
        Task<ManageResult> CreateProduct(ProductDto productDto);

        Task UpdateProduct(int id, ProductDto productDto);
        Task<ManageResult> ModifyProduct(int id, ProductDto productDto);

        Task<Product> GetProductByProductOption(int id);
        Task DeleteProduct(int id);
    }
}