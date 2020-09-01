using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.ProductProcessor;

namespace RefactorThis.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ICreateProductRequestProcessor _createProductRequestProcessor;
        private readonly IGetProductRequestProcessor _getProductRequestProcessor;
        private readonly IUpdateProductRequestProcessor _updateProductRequestProcessor;
        private readonly IDeleteProductRequestProcessor _deleteProductRequestProcessor;

        public ProductsController(
            ICreateProductRequestProcessor createProductRequestProcessor, 
            IGetProductRequestProcessor getProductRequestProcessor,
            IUpdateProductRequestProcessor updateProductRequestProcessor,
            IDeleteProductRequestProcessor deleteProductRequestProcessor)
        {
            _createProductRequestProcessor = createProductRequestProcessor;
            _getProductRequestProcessor = getProductRequestProcessor;
            _updateProductRequestProcessor = updateProductRequestProcessor;
            _deleteProductRequestProcessor = deleteProductRequestProcessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string name)
        {
            var products = await _getProductRequestProcessor.ListProductsAsync(name);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _getProductRequestProcessor.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductRequest productRequest)
        {
            await _createProductRequestProcessor.CreateProductAsync(productRequest);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]ProductRequest product)
        {
            await _updateProductRequestProcessor.UpdateProductAsync(id, product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteProductRequestProcessor.DeleteProductAsync(id);
            return Ok();
        }
    }
}