using System;
using System.Collections.Generic;
using System.Text;

namespace RefactorThis.Core.Domain
{
    public class ProductOptions
    {
        public ProductOptions(List<ProductOption> items)
        {
            Items = items;
        }
        public List<ProductOption> Items { get; }
    }

    public class ProductOption
    {
        public ProductOption(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
    }
}
