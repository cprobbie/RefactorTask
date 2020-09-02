using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RefactorThis.Core.Domain;

namespace RefactorThis.Core.Interfaces
{
    public interface IProductRepository
    {
        public Task<IList<Product>> ListAsync();
        public Task SaveAsync(Product product);
        public Task<IList<Product>> ListAsync(string name);
        public Task<Product> GetAsync(Guid id);
        public Task UpdateAsync(Product product);
        public Task DeleteProductAsync(Product product);
        public Task DeleteOptionAsync(ProductOption option);
        public Task<IList<ProductOption>> ListOptionsAsync(Guid productId);
        public Task<ProductOption> GetOptionAsync(Guid productId, Guid optionId);
        public Task SaveAsync(ProductOption productOption);
        public Task UpdateAsync(ProductOption option);
    }
}
