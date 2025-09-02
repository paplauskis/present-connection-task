using package_tracking_api.Domain.Dtos;

namespace package_tracking_api.Domain.Interfaces;

public interface IPackageService
{
    Task<PackageDto> CreatePackageAsync(CreatePackageDto packageDto);
    Task<IEnumerable<PackageDto>> GetPackagesAsync();
    Task<PackageDto> GetPackageByTrackingNumber(string trackingNumber);
    Task<IEnumerable<PackageDto>> GetFilteredPackagesAsync(PackageFilterDto packageFilterDto);
    Task<PackageDto> UpdatePackageStatusAsync(PackageUpdateStatusDto packageDto);
}