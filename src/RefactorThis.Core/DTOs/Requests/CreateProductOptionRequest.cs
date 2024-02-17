
namespace RefactorThis.Core.Domain.Requests;

public record CreateProductOptionRequest(string Name, string Description) : BaseOptionRequest(Name, Description);