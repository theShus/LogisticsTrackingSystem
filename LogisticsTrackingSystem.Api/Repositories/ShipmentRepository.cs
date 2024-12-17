using LogisticsTrackingSystem.Api.Data;
using LogisticsTrackingSystem.Api.Models;
using LogisticsTrackingSystem.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsTrackingSystem.Api.Repositories;

public class ShipmentRepository : IShipmentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ShipmentRepository> _logger;

    public ShipmentRepository(ApplicationDbContext context, ILogger<ShipmentRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Shipment>> GetAllAsync()
    {
        return await _context.Shipments.ToListAsync();
    }

    public async Task<Shipment?> GetByIdAsync(Guid id)
    {
        return await _context.Shipments.FindAsync(id);
    }

    public async Task<Shipment> CreateAsync(Shipment shipment)
    {
        _context.Shipments.Add(shipment);
        await _context.SaveChangesAsync();
        return shipment;
    }

    public async Task<bool> UpdateAsync(Shipment shipment)
    {
        var existingShipment = await _context.Shipments.FindAsync(shipment.Id);
        if (existingShipment == null)
        {
            return false;
        }

        _context.Entry(existingShipment).CurrentValues.SetValues(shipment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment == null)
        {
            return false;
        }

        _context.Shipments.Remove(shipment);
        await _context.SaveChangesAsync();
        return true;
    }
} 