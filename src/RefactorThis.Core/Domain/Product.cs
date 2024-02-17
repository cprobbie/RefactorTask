using System;
using CSharpFunctionalExtensions;
using RefactorThis.Core.DTOs.Requests;

namespace RefactorThis.Core.Domain;

public class Product
{
    private Product(CreateProductRequest request)
    {
        Id = Guid.NewGuid();
        Name = request.Name;
        Description = request.Description;
        Price = decimal.Round(request.Price, 2);
        DeliveryPrice = decimal.Round(request.DeliveryPrice, 2);
    }

    private Product(Guid id, UpdateProductRequest request)
    {
        Id = id;
        Name = request.Name;
        Description = request.Description;
        Price = decimal.Round(request.Price, 2);
        DeliveryPrice = decimal.Round(request.DeliveryPrice, 2);
    }

    private Product(DTOs.EntityModels.Product entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Description = entity.Description;
        Price = entity.Price;
        DeliveryPrice = entity.DeliveryPrice;
    }
        
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public decimal Price { get; }
    public decimal DeliveryPrice { get; }

    public static Result<Product> CreateProductFromRequest(CreateProductRequest request)
    {
        try
        {
            ValidateRequest(request);
        }
        catch (ArgumentException ex)
        {
            return Result.Failure<Product>(ex.Message);
        }
        return new Product(request);
    }

    public static Result<Product> CreateProductFromUpdateRequest(Guid id, UpdateProductRequest request)
    {
        try
        {
            ValidateRequest(request);
        }
        catch (ArgumentException ex)
        {
            return Result.Failure<Product>(ex.Message);
        }
        return new Product(id, request);
    }

    private static void ValidateRequest(BaseProductRequest request)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Name);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Description);
        if (request.Price <= 0 || request.DeliveryPrice < 0)
        {
            throw new ArgumentException("Invalid amount");
        }
    }

    public static Product? MapFromEntity(DTOs.EntityModels.Product? entity)
    {
        return entity is null ? null : new Product(entity);
    }
}