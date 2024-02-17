using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.DTOs.EntityModels;
using RefactorThis.Core.Interfaces;

namespace RefactorThis.Infrastructure
{
    public class ProductRepository(ProductDbContext dbContext) : IProductRepository
    {
        public async Task<ICollection<Product>> ListAsync() => await dbContext.Products.ToListAsync();

        public async Task<ICollection<ProductOption>> ListOptionsAsync(Guid productId)
        {
            return await dbContext.ProductOptions
                .Where(x => x.ProductId == productId).ToListAsync();
        }
        public async Task<Product?> GetAsync(Guid id)
        {
            return await dbContext.Products.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }
        
        public async Task<Product?> GetAsync(string name)
        {
            return await dbContext.Products
                .SingleOrDefaultAsync(p => p.Name.ToLower() == name.ToLower());
        }

        public async Task<ProductOption?> GetOptionAsync(Guid productId, Guid optionId)
        {
            return await dbContext.ProductOptions.AsNoTracking()
                .SingleOrDefaultAsync(x => x.ProductId == productId && x.Id == optionId);
        }

        public async Task SaveAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task SaveAsync(ProductOption productOption)
        {
            await dbContext.ProductOptions.AddAsync(productOption);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            dbContext.Update(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductOption option)
        {
            dbContext.Update(option);
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(Product product)
        {
            dbContext.Remove(product);
            await dbContext.SaveChangesAsync();
        }
        public async Task DeleteOptionAsync(ProductOption option)
        {
            dbContext.Remove(option);
            await dbContext.SaveChangesAsync();
        }
    }
}
