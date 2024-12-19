using LogisticsTrackingSystem.Api.Data;
using LogisticsTrackingSystem.Api.Exceptions;
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
        try
        {
            return await _context.Shipments
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve all shipments");
            throw new RepositoryException("Failed to retrieve all shipments", ex);
        }
    }

    public async Task<Shipment?> GetByIdAsync(Guid id)
    {
        try
        {
            var shipment = await _context.Shipments
                .AsNoTracking()
                .FirstAsync(s => s.Id == id);

            if (shipment == null)
            {
                throw new EntityNotFoundException($"Shipment with ID {id} was not found");
            }

            return shipment;
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogError(ex, $"Shipment with ID {id} was not found");
            throw new EntityNotFoundException($"Shipment with ID {id} was not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve shipment with ID: {ShipmentId}", id);
            throw new RepositoryException($"Failed to retrieve shipment with ID {id}", ex);
        }
    }

    public async Task<Shipment> CreateAsync(Shipment shipment)
    {
        try
        {
            await _context.Shipments.AddAsync(shipment);
            await _context.SaveChangesAsync();
            return shipment;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Failed to create shipment)");
            throw new RepositoryException("Failed to create shipment", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while creating shipment");
            throw new RepositoryException("An unexpected error occurred while creating shipment", ex);
        }
    }

    public async Task<Shipment> UpdateAsync(Guid id, Shipment shipment)
    {
        try
        {
            var existingShipment = await _context.Shipments
                .FirstAsync(s => s.Id == id);

            if (existingShipment == null)
            {
                _logger.LogWarning("Attempted to update non-existent shipment with ID: {ShipmentId}", id);
                throw new EntityNotFoundException($"Shipment with ID {id} was not found");
            }

            existingShipment.Name = shipment.Name;
            existingShipment.Status = shipment.Status;
            existingShipment.DeliveryDate = shipment.DeliveryDate;

            await _context.SaveChangesAsync();
            
            return existingShipment;
        }
        catch (EntityNotFoundException)
        {
            throw;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database error while updating shipment with ID: {ShipmentId}", id);
            throw new RepositoryException($"Failed to update shipment with ID {id}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while updating shipment with ID: {ShipmentId}", id);
            throw new RepositoryException($"An unexpected error occurred while updating shipment with ID {id}", ex);
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var shipment = await _context.Shipments
                .FirstAsync(s => s.Id == id);

            if (shipment == null)
            {
                throw new EntityNotFoundException($"Shipment with ID {id} was not found");
            }

            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (EntityNotFoundException)
        {
            throw new EntityNotFoundException($"Shipment with ID {id} was not found");
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Failed to delete shipment with ID: {ShipmentId}", id);
            throw new RepositoryException($"Failed to delete shipment with ID {id}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while deleting shipment with ID: {ShipmentId}", id);
            throw new RepositoryException($"An unexpected error occurred while deleting shipment with ID {id}", ex);
        }
    }
}