using System.Collections.Generic;

namespace RefactorThis.Core.Domain.DTOs
{
    public class ProductsDTO
    {
        public IList<Product> Items { get; set; }
        public ProductsDTO(IList<Product> products)
        {
            Items = products;
        }
    }
}
