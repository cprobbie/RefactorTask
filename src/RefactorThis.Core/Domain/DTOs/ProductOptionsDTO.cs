using System.Collections.Generic;

namespace RefactorThis.Core.Domain.DTOs
{
    public class ProductOptionsDto
    {
        public IList<Option> Items { get; set; }
        public ProductOptionsDto(IList<Option> options)
        {
            Items = options;
        }
    }
}
