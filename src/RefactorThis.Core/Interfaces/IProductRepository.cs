using System;
using System.Collections.Generic;
using System.Text;

using RefactorThis.Core.Domain;

namespace RefactorThis.Core.Interfaces
{
    public interface IProductRepository
    {
        public Products List();
        public void Save(Product product);
    }
}
