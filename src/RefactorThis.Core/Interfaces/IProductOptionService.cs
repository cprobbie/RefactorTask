using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using RefactorThis.Core.Domain;
using RefactorThis.Core.Domain.Requests;
using RefactorThis.Core.Domain.Responses;

namespace RefactorThis.Core.Interfaces;

public interface IProductOptionService
{
    Task<ListProductOptionsResponse> ListOptionsAsync(Guid productId);
    Task<Option?> GetOptionByIdAsync(Guid productId, Guid optionId);
    Task<Result<Guid>> CreateProductOptionAsync(Guid productId, CreateProductOptionRequest request);
    Task<Result> UpdateProductOptionAsync(Guid productId, Guid optionId, UpdateProductOptionRequest request);
    Task<Result> DeleteProductOptionAsync(Guid productId, Guid optionId);
}