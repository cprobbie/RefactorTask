using System;
using System.Collections.Generic;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.OptionProcessor
{
    public interface IGetOptionsRequestProcessor
    {
        IList<ProductOption> ListOptions(Guid productId);
        ProductOption GetOptionById(Guid productId, Guid optionId);
    }
    public class GetOptionsRequestProcessor : IGetOptionsRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public GetOptionsRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IList<ProductOption> ListOptions(Guid productId)
        {
            return _productRepository.ListOptions(productId);
        }

        public ProductOption GetOptionById(Guid productId, Guid optionId)
        {
            return _productRepository.GetOption(productId, optionId);
        }
    }
}
