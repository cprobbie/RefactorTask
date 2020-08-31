using System;
using System.Collections.Generic;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.ProductProcessor
{
    public interface IUpdateProductRequestProcessor
    {
        void UpdateProduct(Guid id, ProductRequest product);
    }

    public class UpdateProductRequestProcessor : IUpdateProductRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void UpdateProduct(Guid id, ProductRequest productRequest)
        {
            if (string.IsNullOrWhiteSpace(productRequest.Name) || string.IsNullOrWhiteSpace(productRequest.Description))
            {
                throw new ArgumentException("Invalid input string");
            }

            if (productRequest.Price <= 0 || productRequest.DeliveryPrice < 0)
            {
                throw new ArgumentException("Invalid input amount");
            }

            var existProduct = _productRepository.Get(id);
            if (existProduct is null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            var product = new Product(id, productRequest);
            _productRepository.Update(product);
        }
    }
}
