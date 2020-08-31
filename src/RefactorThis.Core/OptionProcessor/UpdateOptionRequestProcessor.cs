using System;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.OptionProcessor
{
    public interface IUpdateOptionRequestProcessor
    {
        void UpdateProductOption(Guid productId, Guid optionId, ProductOptionRequest option);
    }

    public class UpdateOptionRequestProcessor : IUpdateOptionRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public UpdateOptionRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void UpdateProductOption(Guid productId, Guid optionId, ProductOptionRequest optionRequest)
        {
            if (string.IsNullOrWhiteSpace(optionRequest.Name) || string.IsNullOrWhiteSpace(optionRequest.Description))
            {
                throw new ArgumentException("Invalid input string");
            }
            var existingOption = _productRepository.GetOption(productId, optionId);
            if (existingOption is null)
            {
                throw new ArgumentException("Product Option not found");
            }
            var option = new ProductOption(optionId, productId, optionRequest);
            _productRepository.Update(option);
        }
    }
}
