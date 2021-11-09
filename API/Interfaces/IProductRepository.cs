using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IProductRepository
    {
        Task UpdateProduct(int id, ProductDto productDto);

        Task CreateProduct(ProductDto productDto);

        Task DeleteProduct(int Id);

        Task<Product> GetProductById(int id);
        
        Task<IEnumerable<Product>> GetProducts();

         Task<Product>  GetProductByProductOption(int id);
    }
}