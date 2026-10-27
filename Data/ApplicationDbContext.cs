using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasIndex(p => p.SKU).IsUnique();
        modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.SKU).HasMaxLength(40).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.Category).HasMaxLength(60).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.Supplier).HasMaxLength(100);
        modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(10, 2);
    }
}
