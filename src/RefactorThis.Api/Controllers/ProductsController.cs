﻿using System;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Api.DTOs;
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
        public IActionResult Get([FromQuery]string name)
        {
            var products = _getProductRequestProcessor.ListProducts(name);
            if (products.Count == 0)
            {
                return NotFound("No product was found");
            }
            var productsDto = new ProductsDTO(products);
            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var product = _getProductRequestProcessor.GetProductById(id);
            if (product is null)
            {
                return NotFound($"No product was found for id {id}");
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody]ProductRequest productRequest)
        {
            _createProductRequestProcessor.CreateProduct(productRequest);
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