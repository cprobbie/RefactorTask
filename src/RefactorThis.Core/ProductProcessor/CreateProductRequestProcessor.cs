using System;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.Processor
{
    public interface ICreateProductRequestProcessor
    {
        void CreateProduct(Product product);
    }
    public class CreateProductRequestProcessor : ICreateProductRequestProcessor
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

            var existingProduct = _productRepository.Get(product.Id);
            if (existingProduct is null)
            {
                _productRepository.Save(product);
            }
            else
            {
                throw new ArgumentException("Product Id already exists");
            }
        }
    }
}