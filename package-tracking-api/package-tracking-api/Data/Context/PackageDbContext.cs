using Microsoft.EntityFrameworkCore;
using package_tracking_api.Domain.Models;

namespace package_tracking_api.Data.Context;

public class PackageDbContext : DbContext
{
    public DbSet<Package> Packages { get; set; } 
    public DbSet<PackageStatusHistory> PackageStatuses { get; set; }
    
    public PackageDbContext(DbContextOptions<PackageDbContext> options)
        : base(options) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    { 
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseInMemoryDatabase("Database");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Package>().OwnsOne(p => p.Sender);
        modelBuilder.Entity<Package>().OwnsOne(p => p.Recipient);
    }
}