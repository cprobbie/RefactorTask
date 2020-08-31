using System;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.ProductProcessor
{
    public interface ICreateProductRequestProcessor
    {
        void CreateProduct(ProductRequest product);
    }
    public class CreateProductRequestProcessor : ICreateProductRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public CreateProductRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void CreateProduct(ProductRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Description))
            {
                throw new ArgumentException("Invalid input string");
            }

            if (request.Price <= 0 || request.DeliveryPrice < 0)
            {
                throw new ArgumentException("Invalid input amount");
            }
            var product = new Product(request);
            _productRepository.Save(product);
        }
    }
}