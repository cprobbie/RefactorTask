using System;
using System.Threading.Tasks;

using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.OptionProcessor
{
    public interface IDeleteOptionRequestProcessor
    {
        Task DeleteProductOptionAsync(Guid productId, Guid optionId);
    }
    public class DeleteOptionRequestProcessor : IDeleteOptionRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public DeleteOptionRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task DeleteProductOptionAsync(Guid productId, Guid optionId)
        {
            var existingOption = await _productRepository.GetOptionAsync(productId, optionId);
            if (existingOption is null)
            {
                throw new ArgumentException("Product Option not found");
            }
            await _productRepository.DeleteOptionAsync(optionId);
        }
    }
}
