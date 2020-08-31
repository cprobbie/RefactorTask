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
        ProductOptionsDTO ListOptions(Guid productId);
        ProductOption GetOptionById(Guid productId, Guid optionId);
    }
    public class GetOptionsRequestProcessor : IGetOptionsRequestProcessor
    {
        private readonly IProductRepository _productRepository;

        public GetOptionsRequestProcessor(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductOptionsDTO ListOptions(Guid productId)
        {
            var options = _productRepository.ListOptions(productId);
            if (options is null || !options.Any())
            {
                return null;
            }
            return new ProductOptionsDTO(options);
        }

        public ProductOption GetOptionById(Guid productId, Guid optionId)
        {
            return _productRepository.GetOption(productId, optionId);
        }
    }
}
