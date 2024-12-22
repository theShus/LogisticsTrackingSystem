namespace LogisticsTrackingSystem.Api.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByCredentialsAsync(string username, string password);
} 