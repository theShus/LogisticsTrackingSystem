using LogisticsTrackingSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LogisticsTrackingSystem.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Shipment> Shipments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Seed data with fixed GUIDs for easier testing
        modelBuilder.Entity<Shipment>().HasData(
            new Shipment
            {
                Id = new Guid("8E6B626A-B97B-472B-9E8F-44B5D2EC9CB4"),
                Name = "Test Shipment 1",
                Status = Status.InTransit,
                CreatedAt = DateTime.UtcNow,
                DeliveryDate = DateTime.UtcNow.AddDays(5)
            },
            new Shipment
            {
                Id = new Guid("B98B8AC2-721F-4F06-9DC9-1DEAD7B76A27"),
                Name = "Test Shipment 2",
                Status = Status.InWarehouse,
                CreatedAt = DateTime.UtcNow,
                DeliveryDate = DateTime.UtcNow.AddDays(3)
            }
        );
    }
} 