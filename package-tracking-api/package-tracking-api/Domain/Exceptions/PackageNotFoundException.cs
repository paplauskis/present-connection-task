namespace package_tracking_api.Domain.Exceptions;

public class PackageNotFoundException : Exception
{
    public PackageNotFoundException(string trackingNumber)
        : base($"Package with tracking number {trackingNumber} was not found.") { }
    
    public PackageNotFoundException(Guid id)
        : base($"Package with ID {id} was could not be found.") { }
    
    public PackageNotFoundException()
        : base("No packages were found.") { }
}