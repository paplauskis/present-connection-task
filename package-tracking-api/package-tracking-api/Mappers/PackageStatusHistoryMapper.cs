using package_tracking_api.Domain.Dtos;
using package_tracking_api.Domain.Models;

namespace package_tracking_api.Mappers;

public static class PackageStatusHistoryMapper
{
    public static PackageStatusHistoryDto ToDto(PackageStatusHistory entity)
    {
        return new PackageStatusHistoryDto
        {
            Status = entity.Status,
            CreatedAt = entity.CreatedAt
        };
    }

    public static PackageStatusHistory ToEntity(PackageStatusHistoryDto dto, Package package)
    {
        return new PackageStatusHistory
        {
            PackageId = package.Id,
            Package = package,
            Status = dto.Status,
            CreatedAt = dto.CreatedAt
        };
    }
    
    public static List<PackageStatusHistoryDto> ToDtoList(IEnumerable<PackageStatusHistory> entities)
    {
        return entities.Select(h => ToDto(h)).ToList();
    }
    
    public static List<PackageStatusHistory> ToEntityList(
        IEnumerable<PackageStatusHistoryDto> historyDtos,
        Package package)
    {
        return historyDtos.Select(h => ToEntity(h, package)).ToList();
    }
}