using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface IProductOptionRepository 
    {

        void UpdateProductOption(ProductOption product);

        void CreateProductOption(ProductOption product);

        void DeleteProductOption(int Id);

        Task<ProductOption> GetProductOptionById(int productId, int id);
        Task<IEnumerable<ProductOption>> GetProductOptions(int productId);
    }
}