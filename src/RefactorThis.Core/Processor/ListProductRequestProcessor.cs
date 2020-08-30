using System;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.Processor
{
    public class ListProductRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public ListProductRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Products ListProducts()
        {
            return _productRepository.List();
        }
    }
}