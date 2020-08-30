using System;
using System.Collections.Generic;
using System.Text;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.Processor
{
    public class UpdateProductRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void UpdateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name) || string.IsNullOrWhiteSpace(product.Description))
            {
                throw new ArgumentException("Invalid input string");
            }

            if (product.Price <= 0 || product.DeliveryPrice < 0)
            {
                throw new ArgumentException("Invalid input amount");
            }

            var existProduct = _productRepository.Get(product.Id);
            if (existProduct is null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            _productRepository.Update(product);
        }
    }
}
