using System.Collections.Generic;

namespace RefactorThis.Core.Domain.DTOs
{
    public class ProductsDto
    {
        public IList<Product> Items { get; set; }
        public ProductsDto(IList<Product> products)
        {
            Items = products;
        }
    }
}
