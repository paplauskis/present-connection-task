using package_tracking_api.Domain.Dtos;
using package_tracking_api.Domain.Models;
using package_tracking_api.Helpers;

namespace package_tracking_api.Mappers;

public static class PackageMapper
{
    public static Package MapFromDtoToPackage(PackageDto packageDto)
    {
        var package = new Package
        {
            TrackingNumber = packageDto.TrackingNumber,
            Status = packageDto.Status,
            Sender = packageDto.Sender,
            Recipient = packageDto.Recipient,
            CreatedAt = packageDto.CreatedAt,
        };
        
        package.StatusHistory = PackageStatusHistoryMapper.ToEntityList(
            packageDto.StatusHistory,
            package);
        
        return package;
    }

    public static PackageDto MapPackageToDto(Package package)
    {
        return new PackageDto
        {
            Id = package.Id,
            TrackingNumber = package.TrackingNumber,
            Status = package.Status,
            Sender = package.Sender,
            Recipient =package.Recipient,
            StatusHistory = PackageStatusHistoryMapper.ToDtoList(package.StatusHistory),
            CreatedAt = package.CreatedAt,
            AvailableStatusTransitions = 
                PackageStatusTransitionHelper.GetValidStatusChanges(package.Status)
        };
    }
    
    public static IEnumerable<PackageDto> MapPackagesToDtos(IEnumerable<Package> packages)
    {
        return packages.Select(p => MapPackageToDto(p));
    }
}