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
    public class ProductOptionRepository : IProductOptionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProductOptionRepository(DataContext context, IMapper mapper)
        {
            _context = context;
             _mapper = mapper;
        }
        public async Task CreateProductOption(int productId, ProductOptionDto productOptionDto){

            var productOption = _mapper.Map<ProductOption>(productOptionDto);
           // productOption.productId = productId;
            var product = _context.Products.Include(x => x.ProductOpt).Where(x => x.Id == productId).First();
            product?.ProductOpt?.Add(productOption);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductOption(int id) {            
            var productOption = await _context.ProductOptions.FindAsync(id);
            _context.ProductOptions.Remove(productOption);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductOption(int id, int productId, ProductOptionDto productOptionDto)
        {

            var productOptionNew = _mapper.Map<ProductOption>(productOptionDto);
            productOptionNew.Id = id;
            productOptionNew.productId = productId;
            var productOptionOld = _context.ProductOptions.Include(x => x.Product).ToList().FirstOrDefault(y => y.Id == id);
            _context.Entry(productOptionOld)?.CurrentValues.SetValues(productOptionNew);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductOption> GetProductOptionById(int productId, int id) {
            return await _context.ProductOptions.FirstOrDefaultAsync(x => x.productId == productId && x.Id == id);
        }
        public async Task<IEnumerable<ProductOption>> GetProductOptions(int productId){
            return await _context.ProductOptions.Where(x => x.productId == productId).ToListAsync();

        }
        
    }
}