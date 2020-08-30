using System;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.OptionProcessor
{
    public class ListOptionsRequestProcessor
    {
        private IProductRepository _productRepository;

        public ListOptionsRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductOptions ListOptions(Guid productId)
        {

            return _productRepository.ListOptions(productId);
        }
    }
}
