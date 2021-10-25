using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(DataContext context, IMapper mapper)
        {
            _context = context;
             _mapper = mapper;
        }

        public async Task CreateProduct(ProductDto productDto){

            var product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int Id) {            
            var product = await _context.Products.FindAsync(Id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductById(int id) {
            return await _context.Products.FindAsync(id);
        }
        public async Task<IEnumerable<Product>> GetProducts(){
            return await _context.Products.ToListAsync();

        }

        public async Task UpdateProduct(int id, ProductDto productDto)
        {

            var productNew = _mapper.Map<Product>(productDto);
            productNew.Id = id;
            var productOld = await _context.Products.FindAsync(id);
            _context.Entry(productOld)?.CurrentValues.SetValues(productNew);
            await _context.SaveChangesAsync();
        }
    }
}