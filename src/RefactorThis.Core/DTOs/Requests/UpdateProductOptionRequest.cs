
namespace RefactorThis.Core.Domain.Requests
{
    public record UpdateProductOptionRequest(string Name, string Description) 
        : BaseOptionRequest(Name, Description);
}
