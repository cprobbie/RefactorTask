using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using RefactorThis.Core.Domain;
using RefactorThis.Core.DTOs.EntityModels;
using RefactorThis.Core.DTOs.Requests;
using RefactorThis.Core.DTOs.Responses;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.Services;

public class ProductOptionsService(IProductRepository productRepository) : IProductOptionService
{
    public async Task<ListProductOptionsResponse> ListOptionsAsync(Guid productId)
    {
        var options = await productRepository.ListOptionsAsync(productId);
        return new ListProductOptionsResponse(options.Select(Option.MapFromEntity));
    }

    public async Task<Option?> GetOptionByIdAsync(Guid productId, Guid optionId)
    {
        var entity = await productRepository.GetOptionAsync(productId, optionId);
        return Option.MapFromEntity(entity);
    }

    public async Task<Result<Guid>> CreateProductOptionAsync(Guid productId, CreateProductOptionRequest request)
    {
        var (_, isFailure, option, error) = Option.CreateProductOptionFromRequest(request);

        if (isFailure)
            return Result.Failure<Guid>(error);
        
        var product = await productRepository.GetAsync(productId);
        
        if (product is null)
            return Result.Failure<Guid>("Product does not exist");
        
        await productRepository.SaveAsync(new ProductOption
        {
            Id = option.Id,
            ProductId = productId,
            Name = option.Name,
            Description = option.Description
        });
        return option.Id;
    }

    public async Task<Result> UpdateProductOptionAsync(Guid productId, Guid optionId, UpdateProductOptionRequest request)
    {
        var (_, isFailure, option, error) = Option.CreateOptionFromUpdateRequest(optionId, request);

        if (isFailure)
            return Result.Failure(error);

        var existingOption = await productRepository.GetOptionAsync(productId, optionId);
        if (existingOption is null)
            return Result.Failure($"Product Option {optionId} for Product {productId} does not exists");
        
        await productRepository.UpdateAsync(new ProductOption
        {
            Id = optionId,
            Name = option.Name,
            Description = option.Description,
            ProductId = productId
        });
        return Result.Success();
    }

    public async Task<Result> DeleteProductOptionAsync(Guid productId, Guid optionId)
    {
        var existingOption = await productRepository.GetOptionAsync(productId, optionId);
        if (existingOption is null)
            return Result.Failure($"Product Option {optionId} for Product {productId} does not exists");
        
        await productRepository.DeleteOptionAsync(existingOption);
        return Result.Success();
    }
}