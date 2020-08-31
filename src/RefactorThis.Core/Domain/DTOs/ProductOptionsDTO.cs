using System.Collections.Generic;

namespace RefactorThis.Core.Domain.DTOs
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
