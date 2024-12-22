using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using LogisticsTrackingSystem.Presentation.Configuration;
using LogisticsTrackingSystem.Presentation.Models;
using LogisticsTrackingSystem.Presentation.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace LogisticsTrackingSystem.Presentation.Services;

public class ShipmentService : IShipmentService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;
    private readonly IAuthService _authService;

	public ShipmentService(HttpClient httpClient, IOptions<MyAppSettings> config, IAuthService authService)
    {
        _httpClient = httpClient;
        _apiBaseUrl = config.Value.ApiBaseUrl;
        _authService = authService; 
	}

	private async Task AddAuthHeader()
    {
	    var token = await _authService.GetTokenAsync();
	    if (!string.IsNullOrEmpty(token))
		    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		else _httpClient.DefaultRequestHeaders.Authorization = null;
	}

	public async Task<IEnumerable<Shipment>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Shipment>>($"{_apiBaseUrl}/api/shipments") ?? Enumerable.Empty<Shipment>();
    }

    public async Task<Shipment> GetByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"{_apiBaseUrl}/api/shipments/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Shipment>()
               ?? throw new Exception("Failed to retrieve shipment");
    }

    public async Task<Shipment> CreateAsync(Shipment shipment)
    {
	    if (!await _authService.IsAuthenticatedAsync())
			throw new UnauthorizedAccessException("You must be logged in to create shipments.");

		await AddAuthHeader();

		var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/shipments", shipment);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Shipment>()
               ?? throw new Exception("Failed to create shipment");
    }


    public async Task<Shipment> UpdateAsync(Guid id, Shipment shipment)
    {
	    if (!await _authService.IsAuthenticatedAsync())
		    throw new UnauthorizedAccessException("You must be logged in to delete shipments.");

	    await AddAuthHeader();

		var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/api/shipments/{id}", shipment);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Shipment>()
               ?? throw new Exception("Failed to update shipment");
    }

    public async Task DeleteAsync(Guid id)
    {
	    if (!await _authService.IsAuthenticatedAsync())
		    throw new UnauthorizedAccessException("You must be logged in to update shipments.");

	    await AddAuthHeader();

		var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/api/shipments/{id}");

        Debug.WriteLine(response.ToString());
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