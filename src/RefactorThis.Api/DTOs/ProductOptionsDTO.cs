using System.Collections.Generic;
using RefactorThis.Core.Domain;

namespace RefactorThis.Api.DTOs
{
    public class ProductOptionsDTO
    {
        public IList<ProductOption> Items { get; set; }
        public ProductOptionsDTO(IList<ProductOption> options)
        {
            Items = options;
        }
    }
}
