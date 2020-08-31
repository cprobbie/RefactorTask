using System;
using System.Collections.Generic;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.ProductProcessor
{
    public interface IDeleteProductRequestProcessor
    {
        void DeleteProduct(Guid id);
    }
    public class DeleteProductRequestProcessor : IDeleteProductRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void DeleteProduct(Guid id)
        {
            var existProduct = _productRepository.Get(id);
            if (existProduct is null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            _productRepository.DeleteProduct(id);
        }
    }
}
