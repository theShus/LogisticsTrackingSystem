using LogisticsTrackingSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LogisticsTrackingSystem.Api.Data;

public class ApplicationDbContext : DbContext
{

    //todo promeni bazu na nesto kao H2
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }

    public DbSet<Shipment> Shipments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed some initial data
        modelBuilder.Entity<Shipment>().HasData(
            new Shipment
            {
                Id = Guid.NewGuid(),
                Name = "Test Shipment 1",
                Status = Status.InTransit,
                CreatedAt = DateTime.UtcNow,
                DeliveryDate = DateTime.UtcNow.AddDays(5)
            },
            new Shipment
            {
                Id = Guid.NewGuid(),
                Name = "Test Shipment 2",
                Status = Status.InWarehouse,
                CreatedAt = DateTime.UtcNow,
                DeliveryDate = DateTime.UtcNow.AddDays(3)
            }
        );
    }
} 