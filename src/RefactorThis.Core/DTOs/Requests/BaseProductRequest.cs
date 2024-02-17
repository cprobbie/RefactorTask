namespace RefactorThis.Core.Domain.Requests;

public abstract record BaseProductRequest(string Name, string Description, decimal Price, decimal DeliveryPrice);