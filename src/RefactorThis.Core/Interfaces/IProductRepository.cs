using System;
using RefactorThis.Core.Domain;

namespace RefactorThis.Core.Interfaces
{
    public interface IProductRepository
    {
        public Products List();
        public void Save(Product product);
        public Products List(string name);
        public Product Get(Guid id);
        void Update(Product product);
    }
}
