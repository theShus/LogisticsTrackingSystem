namespace LogisticsTrackingSystem.Presentation.Services.Interfaces;

public interface IAuthService
{
    Task<bool> LoginAsync(string username, string password);
    Task Logout();
	Task<bool> IsAuthenticatedAsync();

	Task<string?> GetTokenAsync();

}