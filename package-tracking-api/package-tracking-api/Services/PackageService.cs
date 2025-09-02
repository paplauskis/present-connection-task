using package_tracking_api.Domain.Dtos;
using package_tracking_api.Domain.Exceptions;
using package_tracking_api.Domain.Interfaces;
using package_tracking_api.Domain.Models;
using package_tracking_api.Helpers;
using package_tracking_api.Mappers;

namespace package_tracking_api.Services;

public class PackageService : IPackageService
{
    private readonly IPackageRepository _packageRepository;
    private readonly IPackageStatusRepository _packageStatusRepository;

    public PackageService(IPackageRepository packageRepository, IPackageStatusRepository packageStatusRepository)
    {
        _packageRepository = packageRepository;
        _packageStatusRepository = packageStatusRepository;
    }

    public async Task<PackageDto> CreatePackageAsync(CreatePackageDto packageDto)
    {
        if (!packageDto.IsValid())
            throw new ArgumentException("Sender and recipient must have name, address and phone.");
        
        var package = new Package
        {
            Sender = packageDto.Sender,
            Recipient = packageDto.Recipient
        };
        
        package.StatusHistory.Add(new PackageStatusHistory
        {
            PackageId = package.Id,
            Package = package,
            Status = package.Status,
            CreatedAt = package.CreatedAt
        });
        
        var addedPackage = await _packageRepository.AddAsync(package);
        
        return PackageMapper.MapPackageToDto(addedPackage);
    }

    public async Task<IEnumerable<PackageDto>> GetPackagesAsync()
    {
        var packages = await _packageRepository.GetAllAsync();

        if (packages.Count == 0)
            throw new PackageNotFoundException();

        return PackageMapper.MapPackagesToDtos(packages.OrderByDescending(p => p.CreatedAt));
    }
    
    public async Task<PackageDto> GetPackageByTrackingNumber(string trackingNumber)
    {
        var package = await _packageRepository.GetByTrackingNumberAsync(trackingNumber);
        
        if (package == null)
            throw new PackageNotFoundException(trackingNumber);
        
        return PackageMapper.MapPackageToDto(package);
    }

    public async Task<IEnumerable<PackageDto>> GetFilteredPackagesAsync(PackageFilterDto packageFilterDto)
    {
        var packages = await _packageRepository.GetFilteredPackagesAsync(packageFilterDto);
        
        if (packages.Count == 0)
            throw new PackageNotFoundException();

        return PackageMapper.MapPackagesToDtos(packages);
    }

    public async Task<PackageDto> UpdatePackageStatusAsync(PackageUpdateStatusDto packageDto)
    {
        var packageToBeUpdated = await _packageRepository.GetByTrackingNumberAsync(packageDto.TrackingNumber);
        
        if (packageToBeUpdated == null)
            throw new PackageNotFoundException(packageDto.TrackingNumber);

        if (!PackageStatusTransitionHelper.CanChangeStatus(
                packageToBeUpdated.Status, packageDto.Status))
        {
            throw new InvalidPackageStatusTransitionException(
                packageToBeUpdated.Status, packageDto.Status);
        }

        var statusHistory = new PackageStatusHistory
        {
            Package = packageToBeUpdated,
            PackageId = packageToBeUpdated.Id,
            Status = packageDto.Status,
        };
        
        packageToBeUpdated.Status = packageDto.Status;
        
        await _packageStatusRepository.AddAsync(statusHistory);
        await _packageRepository.UpdateAsync(packageToBeUpdated);
        
        return PackageMapper.MapPackageToDto(packageToBeUpdated);
    }
}