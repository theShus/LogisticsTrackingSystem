using LogisticsTrackingSystem.Api.Models;

namespace LogisticsTrackingSystem.Api.Services.Interfaces
{
    public interface IShipmentService
    {
        Task<IEnumerable<Shipment>> GetAllAsync();
        Task<Shipment?> GetByIdAsync(Guid id);
        Task<Shipment> CreateAsync(Shipment shipment);
        Task<bool> UpdateAsync(Guid id, Shipment updated);
        Task<bool> DeleteAsync(Guid id);
    }
}
