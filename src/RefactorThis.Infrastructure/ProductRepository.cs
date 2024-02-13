using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IList<Product>> ListAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<IList<Product>> ListAsync(string name)
        {
            return await _dbContext.Products
                .Where(x => x.Name.ToLower() == name.ToLower()).ToListAsync();
        }

        public async Task<IList<Option>> ListOptionsAsync(Guid productId)
        {
            return await _dbContext.Options
                .Where(x => x.ProductId == productId).ToListAsync();
        }
        public async Task<Product> GetAsync(Guid id)
        {
            return await _dbContext.Products.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Option> GetOptionAsync(Guid productId, Guid optionId)
        {
            return await _dbContext.Options.AsNoTracking()
                .SingleOrDefaultAsync(x => x.ProductId == productId && x.Id == optionId);
        }

        public async Task SaveAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveAsync(Option productOption)
        {
            await _dbContext.Options.AddAsync(productOption);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.Update(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Option option)
        {
            _dbContext.Update(option);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(Product product)
        {
            _dbContext.Attach(product);
            _dbContext.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteOptionAsync(Option option)
        {
            _dbContext.Attach(option);
            _dbContext.Remove(option);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveAsync(Product product, ProductOption productOption, Option option)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.ProductOption.AddAsync(productOption);
            await _dbContext.Options.AddAsync(option);
            await _dbContext.SaveChangesAsync();
        }
    }
}
