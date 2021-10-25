using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.DTOs;

namespace API.Interfaces
{
    public interface IProductRepository
    {
        void UpdateProduct(Product product);

        void CreateProduct(Product product);

        void DeleteProduct(int Id);

        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProducts();
    }
}