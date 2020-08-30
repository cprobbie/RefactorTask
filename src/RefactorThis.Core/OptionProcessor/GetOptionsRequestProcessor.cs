using System;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.OptionProcessor
{
    public class GetOptionsRequestProcessor
    {
        private IProductRepository _productRepository;

        public GetOptionsRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductOptions ListOptions(Guid productId)
        {
            return _productRepository.ListOptions(productId);
        }

        public ProductOption GetOptionById(Guid productId, Guid optionId)
        {
            return _productRepository.GetOption(productId, optionId);
        }
    }
}
