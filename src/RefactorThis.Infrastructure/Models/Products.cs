using System;
using System.Collections.Generic;

namespace RefactorThis.Infrastructure.Models
{
    public class Product : ModelBase
    {
        public decimal Price { get; set; }
        public decimal DeliveryPrice { get; set; }
    }
}