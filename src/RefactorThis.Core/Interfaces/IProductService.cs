using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using RefactorThis.Core.Domain;
using RefactorThis.Core.DTOs.Requests;
using RefactorThis.Core.DTOs.Responses;

namespace RefactorThis.Core.Interfaces;

public interface IProductService
{
    Task<ListProductsResponse> ListProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<Result<Guid>> CreateProductAsync(CreateProductRequest product);
    Task<Result> UpdateProductAsync(Guid id, UpdateProductRequest product);
    Task<Result> DeleteProductAsync(Guid id);
    Task<Product?> GetProductByNameAsync(string name);
}