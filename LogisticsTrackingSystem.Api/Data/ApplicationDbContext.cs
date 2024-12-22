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
    public DbSet<User> Users { get; set; }

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

        // Seed user data
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = new Guid("9D9D57C6-F7A6-4659-8B89-95A7E5FD4073"),
                Username = "admin",
                Password = "admin123", // In production, use hashed passwords!
                Role = Role.Admin
            },
            new User
            {
                Id = new Guid("B2BD7ECF-6D56-4B90-9E66-95F3F5D8D65C"),
                Username = "user",
                Password = "user123",
                Role = Role.User
            }
        );
    }
} 