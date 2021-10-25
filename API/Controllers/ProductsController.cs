using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
     [Authorize]
    public class ProductsController : BaseApiController
    {
         private readonly IProductService _productService;
  
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
          var product =  await _productService.GetProducts();
          return Ok(product);
        }
        
        [HttpGet("{id}")]
        public async  Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _productService.GetProductById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDto productDto)
        {
            var ret = productDto.Validate();

            if (ret.Count > 0)
                return BadRequest(ret);

            await _productService.CreateProduct(productDto);

            string returnMessage = $"Product Created";
            return Ok(returnMessage);
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDto productDto)
        {

            if (id == default(int))
                return BadRequest();

            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product does not exist");
            }
            
            var ret = productDto.Validate();
            if (ret.Count > 0 )
                return BadRequest(ret);

            string returnMessage = $"Product Updated";

            await _productService.UpdateProduct(id, productDto);
            return Ok(returnMessage);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == default(int))
                return BadRequest();

            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound("Product does not exist");
            }

            string returnMessage = $"Product Deleted";
            await _productService.DeleteProduct(id);
            return Ok(returnMessage);
        }


    }
}