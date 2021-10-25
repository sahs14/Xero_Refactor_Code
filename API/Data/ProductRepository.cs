using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public void UpdateProduct(Product product) {
              _context.Products.Update(product);
        }

        public void CreateProduct(Product product){
              _context.Products.Add(product);
        }

        public void DeleteProduct(int Id) {            
            var product = _context.Products.FirstOrDefault(x => x.Id == Id);
             _context.Products.Remove(product);}

        public async Task<Product> GetProductById(int id) {
            return await _context.Products.FindAsync(id);
        }
        public async Task<IEnumerable<Product>> GetProducts(){
            return await _context.Products.ToListAsync();

        }
    }
}