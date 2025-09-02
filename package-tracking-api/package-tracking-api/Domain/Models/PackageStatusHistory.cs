namespace package_tracking_api.Domain.Models;

public class PackageStatusHistory : BaseEntity
{
    public Guid PackageId { get; set; }
  
    public required Package Package { get; set; }
    
    public PackageStatus Status { get; set; }
}