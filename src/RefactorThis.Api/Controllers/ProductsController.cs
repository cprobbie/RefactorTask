using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RefactorThis.Core.DTOs.Requests;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController(ILogger<ProductsController> logger, IProductService productService)
        : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery]string? name)
        {
            return Ok(name is null
                ? await productService.ListProductsAsync()
                : await productService.GetProductByNameAsync(name));
        }

        [HttpGet]
        [Route("{id:guid}", Name = "GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await productService.GetProductByIdAsync(id);
            logger.LogInformation("Get product(Id: {id}) successfully", id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]CreateProductRequest productRequest)
        {
            var result = await productService.CreateProductAsync(productRequest);
            
            if (result.IsFailure)
                return BadRequest(result.Error);
            
            logger.LogInformation("Create product(Id: {id}) successfully", result.Value);
            return Created("api/v1/Products", new { id = result.Value });
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdateProductRequest product)
        {
            var result = await productService.UpdateProductAsync(id, product);
            
            if (result.IsFailure)
                return BadRequest(result.Error);
            
            logger.LogInformation("Update product(Id: {id}) successfully", id);
            return Ok();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await productService.DeleteProductAsync(id);
            
            if (result.IsFailure)
                return BadRequest(result.Error);
            
            logger.LogInformation("Delete product(Id: {id}) successfully", id);
            return Ok();
        }
    }
}