using System;

namespace RefactorThis.Core.Domain.EntityModels
{
    public class ProductOption
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Product Product { get; set; } = null!; // Required reference navigation to principal
    }
}