using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.ProductProcessor
{
    public interface IDeleteProductRequestProcessor
    {
        Task DeleteProductAsync(Guid id);
    }
    public class DeleteProductRequestProcessor : IDeleteProductRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var existProduct = await _productRepository.GetAsync(id);
            if (existProduct is null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            await _productRepository.DeleteProductAsync(id);
        }
    }
}
