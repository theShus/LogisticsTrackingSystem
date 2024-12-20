using System.Net.Http.Json;
using LogisticsTrackingSystem.Presentation.Models;
using LogisticsTrackingSystem.Presentation.Services.Interfaces;

namespace LogisticsTrackingSystem.Presentation.Services;

public class ShipmentService : IShipmentService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://localhost:7076/api/shipments";

    public ShipmentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Shipment>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Shipment>>(BaseUrl)
               ?? Enumerable.Empty<Shipment>();
    }

    public async Task<Shipment> GetByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Shipment>()
               ?? throw new Exception("Failed to retrieve shipment");
    }

    public async Task<Shipment> CreateAsync(Shipment shipment)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseUrl, shipment);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Shipment>()
               ?? throw new Exception("Failed to create shipment");
    }

    public async Task<Shipment> UpdateAsync(Guid id, Shipment shipment)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{id}", shipment);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Shipment>()
               ?? throw new Exception("Failed to update shipment");
    }

    public async Task DeleteAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<Shipment>> SearchAsync(string searchTerm, Status? status)
    {
        var shipments = await GetAllAsync();

        return shipments.Where(s =>
            (string.IsNullOrEmpty(searchTerm) || s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) &&
            (!status.HasValue || s.Status == status));
    }
}