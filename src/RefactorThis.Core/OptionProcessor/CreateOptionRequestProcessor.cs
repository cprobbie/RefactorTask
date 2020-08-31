using System;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.OptionProcessor
{
    public interface ICreateOptionRequestProcessor
    {
        void CreateProductOption(Guid productId, ProductOptionRequest option);
    }

    public class CreateOptionRequestProcessor : ICreateOptionRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public CreateOptionRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void CreateProductOption(Guid productId, ProductOptionRequest optionRequest)
        {
            if (string.IsNullOrWhiteSpace(optionRequest.Name) || string.IsNullOrWhiteSpace(optionRequest.Description))
            {
                throw new ArgumentException("Invalid input string");
            }
            var product = _productRepository.Get(productId);
            if (product is null)
            {
                throw new ArgumentException("Product does not exist");
            }

            var option = new ProductOption(productId, optionRequest);
            _productRepository.Save(option);
        }
    }
}
