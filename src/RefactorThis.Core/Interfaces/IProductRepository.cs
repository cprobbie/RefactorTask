using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RefactorThis.Core.Domain;
using RefactorThis.Infrastructure;

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
        public Task DeleteOptionAsync(Option option);
        public Task<IList<Option>> ListOptionsAsync(Guid productId);
        public Task<Option> GetOptionAsync(Guid productId, Guid optionId);
        public Task SaveAsync(Option productOption);
        public Task UpdateAsync(Option option);
        Task SaveAsync(Product product, ProductOption productOption, Option option);
    }
}
