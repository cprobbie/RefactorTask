using System;
using System.Threading.Tasks;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.OptionProcessor
{
    public interface IUpdateOptionRequestProcessor
    {
        Task UpdateProductOptionAsync(Guid productId, Guid optionId, ProductOptionRequest option);
    }

    public class UpdateOptionRequestProcessor : IUpdateOptionRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public UpdateOptionRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task UpdateProductOptionAsync(Guid productId, Guid optionId, ProductOptionRequest optionRequest)
        {
            if (string.IsNullOrWhiteSpace(optionRequest.Name) || string.IsNullOrWhiteSpace(optionRequest.Description))
            {
                throw new ArgumentException("Invalid input string");
            }
            var existingOption = await _productRepository.GetOptionAsync(productId, optionId);
            if (existingOption is null)
            {
                throw new ArgumentException("Product Option not found");
            }
            var option = new ProductOption(optionId, productId, optionRequest);
            await _productRepository.UpdateAsync(option);
        }
    }
}
