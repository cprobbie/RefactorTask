using System.Collections.Generic;
using RefactorThis.Core.Domain;

namespace RefactorThis.Api.DTOs
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
