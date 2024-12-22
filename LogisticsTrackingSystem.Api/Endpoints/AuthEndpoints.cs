using LogisticsTrackingSystem.Api.Services.Interfaces;

namespace LogisticsTrackingSystem.Api.Endpoints;

public class AuthEndpoints
{
    public static void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/api/auth");

        group.MapPost("/login", async (LoginRequest request, IAuthService authService) =>
        {
            var (user, token) = await authService.AuthenticateAsync(request.Username, request.Password);
            
            if (user == null || token == null)
                return Results.Unauthorized();

            return Results.Ok(new { Token = token });
        });
    }
}

public record LoginRequest(string Username, string Password); 