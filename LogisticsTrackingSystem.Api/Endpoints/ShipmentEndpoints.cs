using FluentValidation;
using LogisticsTrackingSystem.Api.Models;
using LogisticsTrackingSystem.Api.Services.Interfaces;

namespace LogisticsTrackingSystem.Api.Endpoints;

public class ShipmentEndpoints
{
    private readonly IShipmentService _shipmentService;
    private readonly ILogger<ShipmentEndpoints> _logger;
    private readonly IValidator<Shipment> _validator;

    public ShipmentEndpoints(
        IShipmentService shipmentService, 
        ILogger<ShipmentEndpoints> logger,
        IValidator<Shipment> validator)
    {
        _shipmentService = shipmentService;
        _logger = logger;
        _validator = validator;
    }

    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        var shipments = app.MapGroup("/api/shipments");

        shipments.MapGet("/", GetAllShipments);
        
        shipments.MapGet("/{id}", GetShipmentById);
        
        shipments.MapPost("/", CreateShipment);
        
        shipments.MapPut("/{id}", UpdateShipment);
        
        shipments.MapDelete("/{id}", DeleteShipment);
    }

    private async Task<IResult> GetAllShipments()
    {
        return Results.Ok(await _shipmentService.GetAllAsync());
    }

    private async Task<IResult> GetShipmentById(Guid id)
    {
        var shipment = await _shipmentService.GetByIdAsync(id);
        return shipment is null ? Results.NotFound() : Results.Ok(shipment);
    }

    private async Task<IResult> CreateShipment(Shipment shipment)
    {
        var validationResult = await _validator.ValidateAsync(shipment);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        var result = await _shipmentService.CreateAsync(shipment);
        return Results.Created($"/api/shipments/{result.Id}", result);
    }

    private async Task<IResult> UpdateShipment(Guid id, Shipment shipment)
    {
        var validationResult = await _validator.ValidateAsync(shipment);
        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        shipment.Id = id;
        var result = await _shipmentService.UpdateAsync(id, shipment);
        return result ? Results.Ok(shipment) : Results.NotFound();
    }

    private async Task<IResult> DeleteShipment(Guid id)
    {
        var result = await _shipmentService.DeleteAsync(id);
        return result ? Results.NoContent() : Results.NotFound();
    }
} 