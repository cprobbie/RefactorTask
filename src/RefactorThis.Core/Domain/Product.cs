using System;

using RefactorThis.Core.Domain.Requests;

namespace RefactorThis.Core.Domain
{
    public class Product
    {
        public Product(ProductRequest request)
        {
            Id = Guid.NewGuid();
            Name = request.Name;
            Description = request.Description;
            Price = request.Price;
            DeliveryPrice = request.DeliveryPrice;
        }

        public Product(Guid id, ProductRequest request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
            Price = request.Price;
            DeliveryPrice = request.DeliveryPrice;
        }

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