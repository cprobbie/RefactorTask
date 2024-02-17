
namespace RefactorThis.Core.DTOs.Requests;

public record CreateProductOptionRequest(string Name, string Description) : BaseOptionRequest(Name, Description);