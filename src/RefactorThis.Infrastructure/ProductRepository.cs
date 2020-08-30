using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

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

        public IList<Product> List()
        {
            return _dbContext.Products.ToList();
        }

        public IList<Product> List(string name)
        {
            return _dbContext.Products.Where(x => x.Name.ToLower() == name.ToLower()).ToList();
        }

        public IList<ProductOption> ListOptions(Guid productId)
        {
            return _dbContext.ProductOptions.Where(x => x.ProductId == productId).ToList();
        }
        public Product Get(Guid id)
        {
            return _dbContext.Products.SingleOrDefault(x => x.Id == id);
        }

        public ProductOption GetOption(Guid productId, Guid optionId)
        {
            return _dbContext.ProductOptions.SingleOrDefault(x => x.ProductId == productId && x.Id == optionId);
        }

        public void Save(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void Save(ProductOption productOption)
        {
            _dbContext.ProductOptions.Add(productOption);
            _dbContext.SaveChanges();
        }

        public void Update(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Update(ProductOption option)
        {
            _dbContext.Entry(option).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public void DeleteProduct(Guid id)
        {
            var product = _dbContext.Products.Find(id);
            _dbContext.Attach(product);
            _dbContext.Remove(product);
            _dbContext.SaveChanges();
        }
        public void DeleteOption(Guid id)
        {
            var option = _dbContext.ProductOptions.Find(id);
            _dbContext.Attach(option);
            _dbContext.Remove(option);
            _dbContext.SaveChanges();
        }


    }
}
