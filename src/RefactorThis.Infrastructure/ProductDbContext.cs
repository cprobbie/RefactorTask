
using Microsoft.EntityFrameworkCore;

using RefactorThis.Infrastructure.Models;

namespace RefactorThis.Infrastructure
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options){}

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
    }

}
