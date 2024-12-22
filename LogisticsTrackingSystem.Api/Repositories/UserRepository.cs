using LogisticsTrackingSystem.Api.Data;
using LogisticsTrackingSystem.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LogisticsTrackingSystem.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByCredentialsAsync(string username, string password)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
    }
} 