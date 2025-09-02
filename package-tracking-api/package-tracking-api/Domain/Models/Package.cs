using package_tracking_api.Helpers;

namespace package_tracking_api.Domain.Models;

public class Package : BaseEntity
{
    public string TrackingNumber { get; set; }
    
    public PackageStatus Status { get; set; }
    
    public required Person Sender { get; set; }
    
    public required Person Recipient { get; set; }

    public List<PackageStatusHistory> StatusHistory { get; set; } = [];

    public Package()
    {
        TrackingNumber = TrackingNumberGenerator.Generate();
        Status = PackageStatus.Created;
    }
}