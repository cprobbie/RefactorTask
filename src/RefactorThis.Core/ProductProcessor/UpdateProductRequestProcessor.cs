using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.ProductProcessor
{
    public interface IUpdateProductRequestProcessor
    {
        Task UpdateProductAsync(Guid id, UpdateProductRequest product);
    }

    public class UpdateProductRequestProcessor : IUpdateProductRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task UpdateProductAsync(Guid id, UpdateProductRequest productRequest)
        {
            if (string.IsNullOrWhiteSpace(productRequest.Name) || string.IsNullOrWhiteSpace(productRequest.Description))
            {
                throw new ArgumentException("Invalid input string");
            }

            if (productRequest.Price <= 0 || productRequest.DeliveryPrice < 0)
            {
                throw new ArgumentException("Invalid input amount");
            }

            var existProduct = await _productRepository.GetAsync(id);
            if (existProduct is null)
            {
                throw new ArgumentException("Product not exists, update failed");
            }

            var product = new Product(id, productRequest);
            await _productRepository.UpdateAsync(product);
        }
    }
}
