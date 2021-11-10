using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Errors;
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
            var product = await _productService.GetProducts();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _productService.GetProductById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDto productDto)
        {
            var result = await _productService.CreateProduct(productDto);
            if (result.IsSuccess)
            {
                return Ok(result.ReturnMessage);
            }
            else
            {
                if (result.errorType == ErrorType.NotFound)
                {
                    return NotFound(result.ReturnMessage);
                }
                else
                {
                    return BadRequest(result.ReturnMessage);
                }
            }

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

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDto productDto)
        {
            var result = await _productService.ModifyProduct(id, productDto);
            if (result.IsSuccess)
            {
                return Ok(result.ReturnMessage);
            }
            else
            {
                if (result.errorType == ErrorType.NotFound)
                {
                    return NotFound(result.ReturnMessage);
                }
                else
                {
                    return BadRequest(result.ReturnMessage);
                }
            }

        }


    }
}