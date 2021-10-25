using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        private readonly DataContext _context;
        public ProductOptionRepository(DataContext context)
        {
            _context = context;
        }

        public void UpdateProductOption(ProductOption productoption) {
             _context.ProductOptions.Update(productoption);
        }

        public void CreateProductOption(ProductOption product){ 
            _context.ProductOptions.Add(product);
        }

        public void DeleteProductOption(int Id) {
            var productOptionInfo = _context.ProductOptions.FirstOrDefault(x => x.Id == Id);
             _context.ProductOptions.Remove(productOptionInfo);
        }

        public async Task<ProductOption> GetProductOptionById(int productId, int id) {
            return await _context.ProductOptions.FirstOrDefaultAsync(x => x.productId == productId && x.Id == id);
        }
        public async Task<IEnumerable<ProductOption>> GetProductOptions(int productId){
            return await _context.ProductOptions.Where(x => x.productId == productId).ToListAsync();

        }
        
    }
}