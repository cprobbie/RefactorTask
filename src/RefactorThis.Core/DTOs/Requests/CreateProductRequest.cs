
namespace RefactorThis.Core.Domain.Requests;

public record CreateProductRequest(string Name, string Description, decimal Price, decimal DeliveryPrice) 
    : BaseProductRequest(Name, Description, Price, DeliveryPrice);