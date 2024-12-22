using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LogisticsTrackingSystem.Api.Repositories.Interfaces;
using LogisticsTrackingSystem.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LogisticsTrackingSystem.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<(User? User, string? Token)> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetByCredentialsAsync(username, password);
        if (user == null) return (null, null);

        var token = GenerateJwtToken(user);
        return (user, token);
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
} 