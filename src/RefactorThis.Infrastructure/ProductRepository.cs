﻿using System;
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
            return await _dbContext.Products.Where(x => x.Name.ToLower() == name.ToLower()).ToListAsync();
        }

        public async Task<IList<ProductOption>> ListOptionsAsync(Guid productId)
        {
            return await _dbContext.ProductOptions.Where(x => x.ProductId == productId).ToListAsync();
        }
        public async Task<Product> GetAsync(Guid id)
        {
            return await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProductOption> GetOptionAsync(Guid productId, Guid optionId)
        {
            return await _dbContext.ProductOptions.SingleOrDefaultAsync(x => x.ProductId == productId && x.Id == optionId);
        }

        public async Task SaveAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveAsync(ProductOption productOption)
        {
            await _dbContext.ProductOptions.AddAsync(productOption);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _dbContext.Entry(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductOption option)
        {
            _dbContext.Entry(option).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            _dbContext.Attach(product);
            _dbContext.Remove(product);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteOptionAsync(Guid id)
        {
            var option = await _dbContext.ProductOptions.FindAsync(id);
            _dbContext.Attach(option);
            _dbContext.Remove(option);
            await _dbContext.SaveChangesAsync();
        }
    }
}
