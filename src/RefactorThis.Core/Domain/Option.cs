using System;
using CSharpFunctionalExtensions;
using RefactorThis.Core.DTOs.EntityModels;
using RefactorThis.Core.DTOs.Requests;

namespace RefactorThis.Core.Domain;

public class Option
{
    private Option(CreateProductOptionRequest optionRequest)
    {
        Id = Guid.NewGuid();
        Name = optionRequest.Name;
        Description = optionRequest.Description;
    }

    private Option(Guid id, UpdateProductOptionRequest optionRequest)
    {
        Id = id;
        Name = optionRequest.Name;
        Description = optionRequest.Description;
    }

    private Option(ProductOption entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Description = entity.Description;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }

    public static Option? MapFromEntity(ProductOption? entity)
    {
        return entity is null ? null : new Option(entity);
    }

    public static Result<Option> CreateProductOptionFromRequest(CreateProductOptionRequest request)
    {
        try
        {
            ValidateRequest(request);
        }
        catch (ArgumentException ex)
        {
            return Result.Failure<Option>(ex.Message);
        }

        return new Option(request);
    }
        
    private static void ValidateRequest(BaseOptionRequest request)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Name);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Description);
    }

    public static Result<Option> CreateOptionFromUpdateRequest(Guid optionId, UpdateProductOptionRequest request)
    {
        try
        {
            ValidateRequest(request);
        }
        catch (ArgumentException ex)
        {
            return Result.Failure<Option>(ex.Message);
        }

        return new Option(optionId, request);
    }
}