using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.DTOs;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.ProductProcessor
{
    public interface IGetProductRequestProcessor
    {
        Task<ProductsDto> ListProductsAsync(string name);
        Task<Product> GetProductByIdAsync(Guid id);
    }
    public class GetProductRequestProcessor : IGetProductRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public GetProductRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductsDto> ListProductsAsync(string name)
        {
            IList<Product> products = string.IsNullOrWhiteSpace(name) ?
                await _productRepository.ListAsync() : 
                await _productRepository.ListAsync(name);

            if (products is null || !products.Any())
            {
                throw new KeyNotFoundException("There is no product");
            }
            return new ProductsDto(products);
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product is null)
            {
                throw new KeyNotFoundException("Product not found");
            }
            return product;
        }
    }
}