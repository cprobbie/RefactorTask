using System;
using System.Collections.Generic;
using System.Linq;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.DTOs;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.ProductProcessor
{
    public interface IGetProductRequestProcessor
    {
        ProductsDto ListProducts(string name);
        Product GetProductById(Guid id);
    }
    public class GetProductRequestProcessor : IGetProductRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public GetProductRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductsDto ListProducts(string name)
        {
            IList<Product> products = string.IsNullOrWhiteSpace(name) ?
                _productRepository.List() : 
                _productRepository.List(name);

            if (products is null || !products.Any())
            {
                return null;
            }
            return new ProductsDto(products);
        }

        public Product GetProductById(Guid id)
        {
            return _productRepository.Get(id);
        }
    }
}