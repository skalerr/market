using Market.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Market.DAL;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // Database.EnsureDeleted();
        // Database.EnsureCreated();
    }
    
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Provider> Providers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Provider>().HasData(
            new Provider { Id = 1, Name = "Tom"},
            new Provider { Id = 2, Name = "Alice"},
            new Provider { Id = 3, Name = "Sam"}
        );
    }
}