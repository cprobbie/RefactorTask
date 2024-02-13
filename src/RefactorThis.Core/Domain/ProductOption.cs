using System;

namespace RefactorThis.Infrastructure
{
    public class ProductOption
    {
        public ProductOption(Guid productFk, Guid optionFk)
        {
            Id = Guid.NewGuid();
            ProductFk = productFk;
            OptionFk = optionFk;
        }
        public Guid Id { get; set; }
        public Guid ProductFk { get; set; }
        public Guid OptionFk { get; set; }
    }
}