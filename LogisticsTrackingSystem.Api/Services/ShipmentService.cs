using LogisticsTrackingSystem.Api.Models;
using LogisticsTrackingSystem.Api.Repositories.Interfaces;
using LogisticsTrackingSystem.Api.Services.Interfaces;

namespace LogisticsTrackingSystem.Api.Services;

public class ShipmentService : IShipmentService
{
    private readonly IShipmentRepository _repository;

    public ShipmentService(IShipmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Shipment>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Shipment?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Shipment> CreateAsync(Shipment shipment)
    {
        return await _repository.CreateAsync(shipment);
    }

    public async Task<Shipment> UpdateAsync(Guid id, Shipment shipment)
    {
        return await _repository.UpdateAsync(id,shipment);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }
}