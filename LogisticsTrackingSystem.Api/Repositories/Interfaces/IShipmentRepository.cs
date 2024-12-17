using LogisticsTrackingSystem.Api.Models;

namespace LogisticsTrackingSystem.Api.Repositories.Interfaces;

public interface IShipmentRepository
{
    Task<IEnumerable<Shipment>> GetAllAsync();
    Task<Shipment?> GetByIdAsync(Guid id);
    Task<Shipment> CreateAsync(Shipment shipment);
    Task<bool> UpdateAsync(Shipment shipment);
    Task<bool> DeleteAsync(Guid id);
} 