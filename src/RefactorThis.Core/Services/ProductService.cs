using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using RefactorThis.Core.Domain;
using RefactorThis.Core.DTOs.Requests;
using RefactorThis.Core.DTOs.Responses;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Core.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<ListProductsResponse> ListProductsAsync()
    {
        var products = await productRepository.ListAsync();
        return new ListProductsResponse(products.Select(Product.MapFromEntity));
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        var productEntity = await productRepository.GetAsync(id);
        return Product.MapFromEntity(productEntity);
    }
    
    public async Task<Product?> GetProductByNameAsync(string name)
    {
        var productEntity = await productRepository.GetAsync(name);
        return Product.MapFromEntity(productEntity);
    }

    public async Task<Result<Guid>> CreateProductAsync(CreateProductRequest request)
    {
        var (_, isFailure, product, error) = Product.CreateProductFromRequest(request);
        
        if (isFailure)
            return Result.Failure<Guid>(error);

        await productRepository.SaveAsync(new DTOs.EntityModels.Product
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            DeliveryPrice = product.DeliveryPrice
        });
        return product.Id;
    }

    public async Task<Result> UpdateProductAsync(Guid id, UpdateProductRequest updateProductRequest)
    {
        var (_, isFailure, product, error) = Product.CreateProductFromUpdateRequest(id, updateProductRequest);

        if (isFailure)
            return Result.Failure(error);
        
        var existProduct = await productRepository.GetAsync(id);
        
        if (existProduct is null)
            return Result.Failure("Product does not exist");
        
        await productRepository.UpdateAsync(new DTOs.EntityModels.Product
        {
            Id = id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            DeliveryPrice = product.DeliveryPrice
        });
        return Result.Success();
    }

    public async Task<Result> DeleteProductAsync(Guid id)
    {
        var existProduct = await productRepository.GetAsync(id);
        
        if (existProduct is null)
            return Result.Failure("Product does not exist");

        var relatedOptions = await productRepository.ListOptionsAsync(id);
        if (relatedOptions.Any())
        {
            foreach (var option in relatedOptions)
            {
                await productRepository.DeleteOptionAsync(option);
            }
        }

        await productRepository.DeleteProductAsync(existProduct);
        return Result.Success();
    }
}