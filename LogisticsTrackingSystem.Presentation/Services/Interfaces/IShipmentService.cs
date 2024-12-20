using LogisticsTrackingSystem.Presentation.Models;

namespace LogisticsTrackingSystem.Presentation.Services.Interfaces;

public interface IShipmentService
{
    Task<IEnumerable<Shipment>> GetAllAsync();
    Task<Shipment> GetByIdAsync(Guid id);
    Task<Shipment> CreateAsync(Shipment shipment);
    Task<Shipment> UpdateAsync(Guid id, Shipment shipment);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Shipment>> SearchAsync(string searchTerm, Status? status);
} 