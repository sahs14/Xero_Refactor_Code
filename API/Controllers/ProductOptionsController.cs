using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
     [Authorize]
    public class ProductOptionsController : BaseApiController
    {
        private readonly DataContext _context;
        public ProductOptionsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{productId}/options")]
        public async Task<ActionResult<List<ProductOption>>> GetProductOptions()
        {
          return await _context.ProductOption.ToListAsync();
        }
        
        [HttpGet("{productId}/options/{id}")]
        public async  Task<ActionResult<ProductOption>> GetProductOption(int id)
        {
            return await _context.ProductOption.FindAsync(id);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateProductOption(ProductOptionDto productDto)
        {
            //product.Save();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductOption(int id, ProductOptionDto productDto)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductOption(int id)
        {
            return Ok();
        }

    }
}