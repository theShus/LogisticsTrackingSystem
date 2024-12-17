using Microsoft.EntityFrameworkCore;
using LogisticsTrackingSystem.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add in-memory database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ShipmentDb"));

// Register services and repositories
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<IShipmentService, ShipmentService>();
builder.Services.AddScoped<ShipmentEndpoints>();

// Add validators
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated(); // This will create and seed the database
}

// Map endpoints
var shipmentEndpoints = app.Services.GetRequiredService<ShipmentEndpoints>();
shipmentEndpoints.MapEndpoints(app);

app.Run();
