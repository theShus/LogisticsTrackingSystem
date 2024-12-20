using System.ComponentModel.DataAnnotations;

namespace LogisticsTrackingSystem.Presentation.Models;

public class Shipment: IValidatableObject
{
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters")]
    public string Name { get; set; } = string.Empty;
    
    public Status Status { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    [Required(ErrorMessage = "Delivery date is required")]
    public DateTime? DeliveryDate { get; set; }


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DeliveryDate?.Date == CreatedAt.Date)
        {
            yield return new ValidationResult(
                "Delivery date cannot be the same as creation date",
                new[] { nameof(DeliveryDate) }
            );
        }
    }
}

public enum Status
{
    InTransit,
    Delivered,
    InWarehouse
} 

