using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Domain;

namespace RefactorThis.Infrastructure
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options){}

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOption { get; set; }
        public DbSet<Option> Options { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Option>().HasKey(x => x.Id);
        }
    }

}
