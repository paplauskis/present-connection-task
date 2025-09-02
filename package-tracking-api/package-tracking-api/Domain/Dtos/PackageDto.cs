using package_tracking_api.Domain.Models;

namespace package_tracking_api.Domain.Dtos;

public class PackageDto
{
    public Guid Id { get; set; }
    public string? TrackingNumber { get; set; }
    
    public PackageStatus Status { get; set; }
    
    public Person? Sender { get; set; }
    
    public Person? Recipient { get; set; }

    public List<PackageStatusHistoryDto>? StatusHistory { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public PackageStatus[]? AvailableStatusTransitions { get; set; }
}