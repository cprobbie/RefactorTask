using System;
using System.Collections.Generic;

namespace RefactorThis.Core.Domain
{
    public class Products
    {
        public List<Product> Items { get; set; }
    }
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DeliveryPrice { get; set; }
    }
}