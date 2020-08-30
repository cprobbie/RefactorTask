using System;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.Processor
{
    public class CreateProductRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public CreateProductRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void CreateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name) || string.IsNullOrWhiteSpace(product.Description))
            {
                throw new ArgumentException("Invalid input string");
            }

            if (product.Price <= 0 || product.DeliveryPrice < 0)
            {
                throw new ArgumentException("Invalid input amount");
            }

            _productRepository.Save(product);
        }
    }
}