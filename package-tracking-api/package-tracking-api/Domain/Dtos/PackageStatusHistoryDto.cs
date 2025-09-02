using package_tracking_api.Domain.Models;

namespace package_tracking_api.Domain.Dtos;

public class PackageStatusHistoryDto
{
    public PackageStatus Status { get; set; }
    
    public DateTime CreatedAt { get; set; }
}