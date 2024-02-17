
namespace RefactorThis.Core.DTOs.Requests
{
    public record UpdateProductOptionRequest(string Name, string Description) 
        : BaseOptionRequest(Name, Description);
}
