using Microsoft.EntityFrameworkCore;
using RefactorThis.Core.Domain.EntityModels;

namespace RefactorThis.Infrastructure;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options){}

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductOption> ProductOptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasMany(e => e.ProductOptions)
            .WithOne(e => e.Product)
            .HasForeignKey(e => e.ProductId)
            .IsRequired();
    }
}