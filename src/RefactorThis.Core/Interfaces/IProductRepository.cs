using System;
using System.Collections.Generic;

using RefactorThis.Core.Domain;

namespace RefactorThis.Core.Interfaces
{
    public interface IProductRepository
    {
        public IList<Product> List();
        public void Save(Product product);
        public IList<Product> List(string name);
        public Product Get(Guid id);
        public void Update(Product product);
        public void DeleteProduct(Guid productId);
        public void DeleteOption(Guid optionId);
        public IList<ProductOption> ListOptions(Guid productId);
        public ProductOption GetOption(Guid productId, Guid optionId);
        public void Save(ProductOption productOption);
        public void Update(ProductOption option);
    }
}
