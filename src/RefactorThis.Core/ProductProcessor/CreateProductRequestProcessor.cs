using System;
using System.Threading.Tasks;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.ProductProcessor
{
    public interface ICreateProductRequestProcessor
    {
        Task CreateProductAsync(ProductRequest product);
    }
    public class CreateProductRequestProcessor : ICreateProductRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public CreateProductRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task CreateProductAsync(ProductRequest request)
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
            await _productRepository.SaveAsync(product);
        }
    }
}