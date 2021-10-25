using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IProductOptionRepository 
    {

        Task CreateProductOption(int productId, ProductOptionDto productOptionDto);

        Task UpdateProductOption(int id, int productId, ProductOptionDto productOptionDto);
        Task DeleteProductOption(int id);

        Task<ProductOption> GetProductOptionById(int productId, int id);
        Task<IEnumerable<ProductOption>> GetProductOptions(int productId);
    }
}