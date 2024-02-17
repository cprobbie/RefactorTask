namespace RefactorThis.Core.DTOs.Requests;

public abstract record BaseProductRequest(
    string Name, 
    string Description, 
    decimal Price, 
    decimal DeliveryPrice);