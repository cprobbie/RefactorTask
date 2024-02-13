
namespace RefactorThis.Core.Domain.Requests
{
    public class UpdateProductRequest
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DeliveryPrice { get; set; }
    }
}
