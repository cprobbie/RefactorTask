using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RefactorThis.Core.Domain.EntityModels;

namespace RefactorThis.Core.Interfaces
{
    public interface IProductRepository
    {
        public Task<ICollection<Product>> ListAsync();
        public Task SaveAsync(Product product);
        public Task<Product?> GetAsync(Guid id);
        public Task<Product?> GetAsync(string name);
        public Task UpdateAsync(Product product);
        public Task DeleteProductAsync(Product product);
        public Task DeleteOptionAsync(ProductOption option);
        public Task<ICollection<ProductOption>> ListOptionsAsync(Guid productId);
        public Task<ProductOption?> GetOptionAsync(Guid productId, Guid optionId);
        public Task SaveAsync(ProductOption productOption);
        public Task UpdateAsync(ProductOption option);
    }
}
