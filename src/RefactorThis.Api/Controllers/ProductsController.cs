using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.ProductProcessor;

namespace RefactorThis.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ICreateProductRequestProcessor _createProductRequestProcessor;
        private readonly IGetProductRequestProcessor _getProductRequestProcessor;
        private readonly IUpdateProductRequestProcessor _updateProductRequestProcessor;
        private readonly IDeleteProductRequestProcessor _deleteProductRequestProcessor;

        public ProductsController(ILogger<ProductsController> logger,
            ICreateProductRequestProcessor createProductRequestProcessor, 
            IGetProductRequestProcessor getProductRequestProcessor,
            IUpdateProductRequestProcessor updateProductRequestProcessor,
            IDeleteProductRequestProcessor deleteProductRequestProcessor)
        {
            _logger = logger;
            _createProductRequestProcessor = createProductRequestProcessor;
            _getProductRequestProcessor = getProductRequestProcessor;
            _updateProductRequestProcessor = updateProductRequestProcessor;
            _deleteProductRequestProcessor = deleteProductRequestProcessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string name)
        {
            var products = await _getProductRequestProcessor.ListProductsAsync(name);
            _logger.LogInformation("List products successfully");
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _getProductRequestProcessor.GetProductByIdAsync(id);
            _logger.LogInformation($"Get product(Id: {id}) successfully");
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateProductRequest productRequest)
        {
            await _createProductRequestProcessor.CreateProductAsync(productRequest);
            _logger.LogInformation("Create product successfully");
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdateProductRequest product)
        {
            await _updateProductRequestProcessor.UpdateProductAsync(id, product);
            _logger.LogInformation($"Update product(Id: {id}) successfully");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _deleteProductRequestProcessor.DeleteProductAsync(id);
            _logger.LogInformation($"Delete product(Id: {id}) successfully");
            return Ok();
        }
    }
}