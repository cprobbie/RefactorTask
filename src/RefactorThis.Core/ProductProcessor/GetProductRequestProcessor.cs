using System;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.Processor
{
    public interface IGetProductRequestProcessor
    {
        Products ListProducts();
        Products ListProducts(string name);
        Product GetProductById(Guid id);
    }
    public class GetProductRequestProcessor : IGetProductRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public GetProductRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Products ListProducts()
        {
            return _productRepository.List();
        }

        public Products ListProducts(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid Product name");
            }
            return _productRepository.List(name);
        }

        public Product GetProductById(Guid id)
        {
            return _productRepository.Get(id);
        }
    }
}