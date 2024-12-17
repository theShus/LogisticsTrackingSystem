namespace LogisticsTrackingSystem.Api.Models;

public class Shipment
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeliveryDate { get; set; }
}
