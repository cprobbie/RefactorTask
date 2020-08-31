using System;
using System.Text.Json.Serialization;

using RefactorThis.Core.Domain.Requests;

namespace RefactorThis.Core.Domain
{
    public class ProductOption
    {
        public ProductOption(Guid productId, ProductOptionRequest optionRequest)
        {
            Id = Guid.NewGuid();
            Name = optionRequest.Name;
            Description = optionRequest.Description;
            ProductId = productId;
        }

        public ProductOption(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public ProductOption(Guid id, Guid productId, ProductOptionRequest optionRequest)
        {
            Id = id;
            Name = optionRequest.Name;
            Description = optionRequest.Description;
            ProductId = productId;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public Guid ProductId { get; set; }
    }
}
