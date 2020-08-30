using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RefactorThis.Core.Domain
{
    public class ProductOption
    {
        public ProductOption(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public Guid ProductId { get; set; }
    }
}
