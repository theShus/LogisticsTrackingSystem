using FluentValidation;
using Microsoft.EntityFrameworkCore;
using LogisticsTrackingSystem.Api.Data;
using LogisticsTrackingSystem.Api.Endpoints;
using LogisticsTrackingSystem.Api.Repositories;
using LogisticsTrackingSystem.Api.Repositories.Interfaces;
using LogisticsTrackingSystem.Api.Services;
using LogisticsTrackingSystem.Api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add CORS configuration - add this before other service configurations
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add in-memory database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ShipmentDb"));

// Register services and repositories
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<IShipmentService, ShipmentService>();

// Add validators
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Add JWT configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!)
            )
        };
    });

builder.Services.AddAuthorization();

// Register services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Enable CORS - add this before other middleware (before UseAuthorization)
app.UseCors("AllowBlazorApp");

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

ShipmentEndpoints.MapEndpoints(app);
AuthEndpoints.MapEndpoints(app);

app.UseAuthentication();
app.UseAuthorization();

app.Run();