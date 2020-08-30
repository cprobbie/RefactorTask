using System;

namespace RefactorThis.Core.Domain
{
    public class Product
    {
        public Product(Guid id, string name, string description, decimal price, decimal deliveryPrice)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            DeliveryPrice = deliveryPrice;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DeliveryPrice { get; set; }
    }
}