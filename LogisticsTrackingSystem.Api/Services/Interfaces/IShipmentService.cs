using LogisticsTrackingSystem.Api.Models;

namespace LogisticsTrackingSystem.Api.Services.Interfaces
{
    public interface IShipmentService
    {
        Task<IEnumerable<Shipment>> GetAllAsync();
        Task<Shipment?> GetByIdAsync(Guid id);
        Task<Shipment> CreateAsync(Shipment shipment);
        Task<Shipment> UpdateAsync(Guid id, Shipment shipment);
        Task<bool> DeleteAsync(Guid id);
    }
}
