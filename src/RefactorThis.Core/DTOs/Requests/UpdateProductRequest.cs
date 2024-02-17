
namespace RefactorThis.Core.DTOs.Requests;

public record UpdateProductRequest(string Name, string Description, decimal Price, decimal DeliveryPrice) 
    : BaseProductRequest(Name, Description, Price, DeliveryPrice);