﻿using System;
using System.Collections.Generic;

namespace RefactorThis.Core.DTOs.EntityModels;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal DeliveryPrice { get; set; }
    
    // One to many relationship. One product has many options
    public ICollection<ProductOption> ProductOptions { get; set; } // Collection navigation
}