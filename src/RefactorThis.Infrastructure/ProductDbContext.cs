using System;

using Microsoft.EntityFrameworkCore;

namespace RefactorThis.Infrastructure
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options){}

        public DbSet<Product> Product { get; set; }
    }
}
