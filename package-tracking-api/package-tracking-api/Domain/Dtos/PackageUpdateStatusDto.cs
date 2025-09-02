using package_tracking_api.Domain.Models;

namespace package_tracking_api.Domain.Dtos;

public class PackageUpdateStatusDto
{
    public required string TrackingNumber { get; set; }
    public required PackageStatus Status { get; set; }
}