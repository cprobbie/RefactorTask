using System;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.OptionProcessor
{
    public interface ICreateOptionRequestProcessor
    {
        void CreateProductOption(Guid productId, ProductOption option);
    }

    public class CreateOptionRequestProcessor : ICreateOptionRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public CreateOptionRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void CreateProductOption(Guid productId, ProductOption option)
        {
            if (string.IsNullOrWhiteSpace(option.Name) || string.IsNullOrWhiteSpace(option.Description))
            {
                throw new ArgumentException("Invalid input string");
            }
            var product = _productRepository.Get(productId);
            if (product is null)
            {
                throw new ArgumentException("Product does not exist");
            }
            var existingOption = _productRepository.GetOption(productId, option.Id);
            if (existingOption != null)
            {
                throw new ArgumentException("Product Option Id already exists");
            }
            
            _productRepository.Save(option);
        }
    }
}
