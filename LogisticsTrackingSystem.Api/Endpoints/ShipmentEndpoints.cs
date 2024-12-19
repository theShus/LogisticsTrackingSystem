using FluentValidation;
using LogisticsTrackingSystem.Api.Models;
using LogisticsTrackingSystem.Api.Services.Interfaces;
using LogisticsTrackingSystem.Api.Exceptions;

namespace LogisticsTrackingSystem.Api.Endpoints;

public class ShipmentEndpoints
{

    public ShipmentEndpoints()
    {
    }

    public static void MapEndpoints(WebApplication app)
    {
        var endpointGroup = app.MapGroup("/api/shipments");

        //GetAllShipments
        endpointGroup.MapGet("/", async (IShipmentService service) => {
            try
            {
                var shipments = await service.GetAllAsync();
                return Results.Ok(shipments);
            }
            catch (RepositoryException ex)
            {
                return Results.Problem(
                    title: "Database Error",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
            catch (Exception ex)
            {
                return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        //GetShipmentById
        endpointGroup.MapGet("/{id}", async (Guid id, IShipmentService service) => {
            try
            {
                var shipment = await service.GetByIdAsync(id);
                return Results.Ok(shipment);
            }
            catch (EntityNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (RepositoryException ex)
            {
                return Results.Problem(
                    title: "Database Error",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
            catch (Exception ex)
            {
                return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        //CreateShipment
        endpointGroup.MapPost("/", async (Shipment shipment, IShipmentService service, IValidator < Shipment > validator) => {
            try
            {
                var validationResult = await validator.ValidateAsync(shipment);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(
                        validationResult.ToDictionary()
                    );
                }

                var result = await service.CreateAsync(shipment);
                return Results.Created($"/api/shipments/{result.Id}", result);
            }
            catch (RepositoryException ex)
            {
                return Results.Problem(
                    title: "Database Error",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
            catch (Exception ex)
            {
                return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        //UpdateShipment
        endpointGroup.MapPut("/{id}", async (Guid id, Shipment shipment, IShipmentService service, IValidator<Shipment> validator) => {
            try
            {
                if (id != shipment.Id)
                {
                    return Results.BadRequest(new { message = "ID in route must match ID in shipment" });
                }

                var validationResult = await validator.ValidateAsync(shipment);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                var updatedShipment = await service.UpdateAsync(id, shipment);
                return Results.Ok(updatedShipment);
            }
            catch (EntityNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (RepositoryException ex)
            {
                return Results.Problem(
                    title: "Database Error",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
            catch (Exception ex)
            {
                return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        });

        //DeleteShipment
        endpointGroup.MapDelete("/{id}", async (Guid id, IShipmentService service) => {
            try
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (RepositoryException ex)
            {
                return Results.Problem(
                    title: "Database Error",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError
                );
            }
            catch (Exception ex)
            {
                return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
            }
        });

    }
}