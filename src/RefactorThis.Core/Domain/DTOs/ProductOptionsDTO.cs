using System.Collections.Generic;

namespace RefactorThis.Core.Domain.DTOs
{
    public class ProductOptionsDto
    {
        public IList<ProductOption> Items { get; set; }
        public ProductOptionsDto(IList<ProductOption> options)
        {
            Items = options;
        }
    }
}
