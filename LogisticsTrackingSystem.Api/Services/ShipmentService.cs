using LogisticsTrackingSystem.Api.Models;
using LogisticsTrackingSystem.Api.Repositories.Interfaces;
using LogisticsTrackingSystem.Api.Services.Interfaces;

namespace LogisticsTrackingSystem.Api.Services;

public class ShipmentService : IShipmentService
{
    private readonly IShipmentRepository _repository;
    private readonly ILogger<ShipmentService> _logger;

    public ShipmentService(IShipmentRepository repository, ILogger<ShipmentService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Shipment>> GetAllAsync()
    {
        _logger.LogInformation("Getting all shipments");
        return await _repository.GetAllAsync();
    }

    public async Task<Shipment?> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("Getting shipment with ID: {Id}", id);
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Shipment> CreateAsync(Shipment shipment)
    {
        _logger.LogInformation("Creating new shipment");
        shipment.CreatedAt = DateTime.UtcNow;
        return await _repository.CreateAsync(shipment);
    }

    public async Task<bool> UpdateAsync(Guid id, Shipment shipment)
    {
        _logger.LogInformation("Updating shipment with ID: {Id}", id);
        shipment.Id = id;
        return await _repository.UpdateAsync(shipment);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        _logger.LogInformation("Deleting shipment with ID: {Id}", id);
        return await _repository.DeleteAsync(id);
    }
}

