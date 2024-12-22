namespace LogisticsTrackingSystem.Api.Services.Interfaces;

public interface IAuthService
{
    Task<(User? User, string? Token)> AuthenticateAsync(string username, string password);
} 