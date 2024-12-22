using Blazored.LocalStorage;
using LogisticsTrackingSystem.Presentation.Configuration;
using LogisticsTrackingSystem.Presentation.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace LogisticsTrackingSystem.Presentation.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    private readonly string _apiBaseUrl;

    public static string TOKEN_KEY = "jwtToken";

	public AuthService(HttpClient httpClient, IOptions<MyAppSettings> config, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _apiBaseUrl = config.Value.ApiBaseUrl;
        _jsRuntime = jsRuntime;
	}

    public async Task<bool> LoginAsync(string username, string password)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/api/auth/login", new
            {
                Username = username,
                Password = password
            });

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadFromJsonAsync<TokenResponse>();
                if (token?.Token == null) return false;
                // Persist to localStorage
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, token.Token);
					
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task Logout()
    {
	    await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TOKEN_KEY);
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
	    var storedToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TOKEN_KEY);
	    return !string.IsNullOrEmpty(storedToken);
    }

    public async Task<string?> GetTokenAsync()
    {
	    var storedToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TOKEN_KEY);
	    return string.IsNullOrEmpty(storedToken) ? null : storedToken;
    }
}

public class TokenResponse
{
	public string Token { get; set; } = string.Empty;
}
