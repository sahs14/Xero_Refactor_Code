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
    public class ProductOptionsController : BaseApiController
    {
         private readonly IProductOptionService _productOptionService;
        public ProductOptionsController(IProductOptionService productOptionService)
        {
           _productOptionService = productOptionService;
        } 

        [HttpGet("{productId}/options")]
        public async Task<ActionResult<IEnumerable<ProductOption>>> GetProductOptions(int productId)
        {
          var result = await _productOptionService.GetProductOptions(productId);
          return Ok(result);
        }
        
        [HttpGet("{productId}/options/{id}")]
        public async  Task<ActionResult<ProductOption>> GetProductOption(int productId, int id)
        {
            return await _productOptionService.GetProductOptionById(productId, id);
        }
        
        [HttpPost("{productId}/options")]
        public async Task<ActionResult> Post(int productId, [FromBody] ProductOptionDto productOptionDto)
        {
            var result = await _productOptionService.CreateProductOption(productId, productOptionDto);
            if(result.IsSuccess){
                return Ok(result.ReturnMessage);
            }
            else{
                if(result.errorType == ErrorType.NotFound){
                    return NotFound(result.ReturnMessage);
                }
                else
                {
                    return BadRequest(result.ReturnMessage);
                }
            }
           
        }

        [HttpPut("{productId}/options/{id}")]
        public async Task<ActionResult> Put(int id, int productId, [FromBody] ProductOptionDto productOptionDto)
        {
            if (id == default(int) || productId == default(int))
                return BadRequest();

            var productOption = await _productOptionService.GetProductOptionById(productId, id);
            if (productOption == null)
            {
                return NotFound("Product does not exist");
            }

            var ret = productOptionDto.Validate();
            if (ret.Count > 0)
                return BadRequest(ret);

            await _productOptionService.UpdateProductOption(id, productId, productOptionDto);
            return Ok();
        }

        [HttpDelete("{productId}/options/{id}")]
        public async Task<ActionResult> Delete(int id, int productId)
        {
            if (id == default(int))
                return BadRequest();

            var productOption = await _productOptionService.GetProductOptionById(productId, id);
            if (productOption == null)
            {
                return NotFound("Product does not exist");
            }

            await _productOptionService.DeleteProductOption(id);
            return Ok();
        }

    }
}