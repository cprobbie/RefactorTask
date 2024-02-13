using System;
using System.Text.Json.Serialization;

using RefactorThis.Core.Domain.Requests;

namespace RefactorThis.Core.Domain
{
    public class Option
    {
        public Option()
        {
            Id = Guid.NewGuid();
            Name = "option";
            Description = "some description";
        }
        public Option(Guid productId, CreateProductOptionRequest optionRequest)
        {
            Id = Guid.NewGuid();
            Name = optionRequest.Name;
            Description = optionRequest.Description;
            ProductId = productId;
        }

        public Option(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Option(Guid id, Guid productId, UpdateProductOptionRequest optionRequest)
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
