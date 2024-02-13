using System.Collections.Generic;
using System.Linq;

namespace RefactorThis.Core.Domain.DTOs
{
    public class ProductsDto
    {
        public IList<ProductDto> Items { get; set; }
        public ProductsDto(IList<Product> products)
        {
            Items = products.Select(p => new ProductDto(p)).ToList();
        }
    }

    public class ProductDto
    {
        public decimal Price { get; set; }
        public ProductDto(Product product)
        {
            Price = decimal.Round(product.Price, 2);
        }
    }
}
