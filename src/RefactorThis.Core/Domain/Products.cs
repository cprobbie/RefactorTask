using System;
using System.Collections.Generic;

namespace RefactorThis.Core.Domain
{
    public class Products
    {
        public Products()
        {

        }
        public Products(List<Product> items)
        {
            Items = items;
        }
        public List<Product> Items { get; }
    }
    public class Product
    {
        public Product()
        {

        }
        public Product(Guid id, string name, string description, decimal price, decimal deliveryPrice)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            DeliveryPrice = deliveryPrice;
        }
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }
        public decimal DeliveryPrice { get; }
    }
}