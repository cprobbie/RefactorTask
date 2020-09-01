using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.DTOs;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.OptionProcessor
{
    public interface IGetOptionsRequestProcessor
    {
        Task<ProductOptionsDto> ListOptionsAsync(Guid productId);
        Task<ProductOption> GetOptionByIdAsync(Guid productId, Guid optionId);
    }
    public class GetOptionsRequestProcessor : IGetOptionsRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public GetOptionsRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductOptionsDto> ListOptionsAsync(Guid productId)
        {
            var options = await _productRepository.ListOptionsAsync(productId);
            if (options is null || !options.Any())
            {
                throw new KeyNotFoundException("There is no option available");
            }
            return new ProductOptionsDto(options);
        }

        public async Task<ProductOption> GetOptionByIdAsync(Guid productId, Guid optionId)
        {
            var option = await _productRepository.GetOptionAsync(productId, optionId);
            if (option is null)
            {
                throw new KeyNotFoundException("Option not found");
            }
            return option;
        }
    }
}
