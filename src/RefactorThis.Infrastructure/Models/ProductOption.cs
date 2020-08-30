using System;
using System.Text.Json.Serialization;

namespace RefactorThis.Infrastructure.Models
{
    public class ProductOption : BaseModel
    {
        public Guid ProductId { get; set; }
    }
}