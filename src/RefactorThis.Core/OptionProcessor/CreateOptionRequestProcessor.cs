using System;
using System.Threading.Tasks;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.OptionProcessor
{
    public interface ICreateOptionRequestProcessor
    {
        Task CreateProductOptionAsync(Guid productId, CreateProductOptionRequest option);
    }

    public class CreateOptionRequestProcessor : ICreateOptionRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public CreateOptionRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task CreateProductOptionAsync(Guid productId, CreateProductOptionRequest optionRequest)
        {
            if (string.IsNullOrWhiteSpace(optionRequest.Name) || string.IsNullOrWhiteSpace(optionRequest.Description))
            {
                throw new ArgumentException("Invalid input string");
            }
            var product = await _productRepository.GetAsync(productId);
            if (product is null)
            {
                throw new ArgumentException("Product does not exist");
            }

            var option = new ProductOption(productId, optionRequest);
            await _productRepository.SaveAsync(option);
        }
    }
}
