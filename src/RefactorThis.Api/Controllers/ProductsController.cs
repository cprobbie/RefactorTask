using System;

using Microsoft.AspNetCore.Mvc;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Processor;

namespace RefactorThis.Controllers
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
        public IActionResult Get()
        {
            var products = _getProductRequestProcessor.ListProducts();
            return Ok(products);
        }

        [HttpGet]
        public IActionResult Get([FromQuery]string name)
        {
            var products = _getProductRequestProcessor.ListProducts(name);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var product = _getProductRequestProcessor.GetProductById(id);
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            _createProductRequestProcessor.CreateProduct(product);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody]Product product)
        {
            _updateProductRequestProcessor.UpdateProduct(id, product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _deleteProductRequestProcessor.DeleteProduct(id);
            return Ok();
        }
    }
}