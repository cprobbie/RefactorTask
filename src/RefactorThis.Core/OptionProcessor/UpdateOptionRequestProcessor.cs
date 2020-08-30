using System;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.OptionProcessor
{
    public interface IUpdateOptionRequestProcessor
    {
        void UpdateProductOption(Guid productId, Guid optionId, ProductOption option);
    }

    public class UpdateOptionRequestProcessor : IUpdateOptionRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public UpdateOptionRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void UpdateProductOption(Guid productId, Guid optionId, ProductOption option)
        {
            if (string.IsNullOrWhiteSpace(option.Name) || string.IsNullOrWhiteSpace(option.Description))
            {
                throw new ArgumentException("Invalid input string");
            }
            var existingOption = _productRepository.GetOption(productId, optionId);
            if (existingOption is null)
            {
                throw new ArgumentException("Product Option not found");
            }

            _productRepository.Update(option);
        }
    }
}
