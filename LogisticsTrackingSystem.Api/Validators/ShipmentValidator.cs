using FluentValidation;
using LogisticsTrackingSystem.Api.Models;

namespace LogisticsTrackingSystem.Api.Validators;

public class ShipmentValidator : AbstractValidator<Shipment>
{
    public ShipmentValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("Name cannot be empty.");

        RuleFor(s => s.DeliveryDate)
            .Must((shipment, date) => !date.HasValue || date > DateTime.UtcNow)
            .WithMessage("DeliveryDate must be in the future if provided.");
    }
}

