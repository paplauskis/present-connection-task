using package_tracking_api.Domain.Models;

namespace package_tracking_api.Domain.Dtos;

public class PackageFilterDto
{
    // public string? TrackingNumber { get; set; }
    public PackageStatus? Status { get; set; }
    
    public string? SenderName { get; set; }
    
    public string? RecipientName { get; set; }
}