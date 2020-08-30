using System;
using System.Collections.Generic;
using System.Text;

using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.OptionProcessor
{
    public interface IDeleteOptionRequestProcessor
    {
        void DeleteProductOption(Guid productId, Guid optionId);
    }
    public class DeleteOptionRequestProcessor : IDeleteOptionRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public DeleteOptionRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void DeleteProductOption(Guid productId, Guid optionId)
        {
            var existingOption = _productRepository.GetOption(productId, optionId);
            if (existingOption is null)
            {
                throw new ArgumentException("Product Option not found");
            }
            _productRepository.Delete(optionId);
        }
    }
}
