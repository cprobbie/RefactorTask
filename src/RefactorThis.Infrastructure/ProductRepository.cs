using System;
using System.Collections.Generic;
using System.Text;

using RefactorThis.Core.Domain;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Product Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProductOption GetOption(Guid productId, Guid optionId)
        {
            throw new NotImplementedException();
        }

        public Products List()
        {
            throw new NotImplementedException();
        }

        public Products List(string name)
        {
            throw new NotImplementedException();
        }

        public ProductOptions ListOptions(Guid productId)
        {
            throw new NotImplementedException();
        }

        public void Save(Product product)
        {
            throw new NotImplementedException();
        }

        public void Save(ProductOption productOption)
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductOption option)
        {
            throw new NotImplementedException();
        }
    }
}
