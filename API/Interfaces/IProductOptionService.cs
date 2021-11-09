using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Errors;

namespace API.Interfaces
{
    public interface IProductOptionService
    {
        Task<ProductOption> GetProductOptionById(int productId, int id);
        Task<IEnumerable<ProductOption>> GetProductOptions(int productId);

        Task<ManageResult> CreateProductOption(int productId, ProductOptionDto productOptionDto);

        Task UpdateProductOption(int id, int productId, ProductOptionDto productOptionDto);
        Task DeleteProductOption(int id);

        Task<ManageResult> ModifyProductOption(int id, int productId, ProductOptionDto productOptionDto);
        
    }
}