﻿using System;
using System.Collections.Generic;
using System.Linq;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.DTOs;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.OptionProcessor
{
    public interface IGetOptionsRequestProcessor
    {
        ProductOptionsDto ListOptions(Guid productId);
        ProductOption GetOptionById(Guid productId, Guid optionId);
    }
    public class GetOptionsRequestProcessor : IGetOptionsRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public GetOptionsRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductOptionsDto ListOptions(Guid productId)
        {
            var options = _productRepository.ListOptions(productId);
            if (options is null || !options.Any())
            {
                throw new KeyNotFoundException("There is no option available");
            }
            return new ProductOptionsDto(options);
        }

        public ProductOption GetOptionById(Guid productId, Guid optionId)
        {
            var option = _productRepository.GetOption(productId, optionId);
            if (option is null)
            {
                throw new KeyNotFoundException("Option not found");
            }
            return option;
        }
    }
}
