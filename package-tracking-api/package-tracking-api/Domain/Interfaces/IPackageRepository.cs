using package_tracking_api.Domain.Dtos;
using package_tracking_api.Domain.Models;

namespace package_tracking_api.Domain.Interfaces;

public interface IPackageRepository : IRepository<Package>
{
    Task<List<Package>> GetFilteredPackagesAsync(PackageFilterDto filters);
    Task<Package?> GetByTrackingNumberAsync(string trackingNumber);
}