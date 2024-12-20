using FluentValidation;
using Microsoft.EntityFrameworkCore;
using LogisticsTrackingSystem.Api.Data;
using LogisticsTrackingSystem.Api.Endpoints;
using LogisticsTrackingSystem.Api.Repositories;
using LogisticsTrackingSystem.Api.Repositories.Interfaces;
using LogisticsTrackingSystem.Api.Services;
using LogisticsTrackingSystem.Api.Services.Interfaces;

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

app.Run();