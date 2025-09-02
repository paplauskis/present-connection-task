using package_tracking_api.Domain.Models;

namespace package_tracking_api.Helpers;

public static class PackageStatusTransitionHelper
{
    private static readonly Dictionary<PackageStatus, PackageStatus[]> ValidStatusChanges = new()
    {
        [PackageStatus.Created] = new[] { PackageStatus.Sent, PackageStatus.Cancelled },
        [PackageStatus.Sent] = new[] { PackageStatus.Accepted, PackageStatus.Returned, PackageStatus.Cancelled },
        [PackageStatus.Returned] = new[] { PackageStatus.Sent, PackageStatus.Cancelled },
        [PackageStatus.Accepted] = new PackageStatus[0],
        [PackageStatus.Cancelled] = new PackageStatus[0],
    };

    public static PackageStatus[] GetValidStatusChanges(PackageStatus status)
    {
        return ValidStatusChanges[status];
    }

    public static bool CanChangeStatus(PackageStatus status, PackageStatus newStatus)
    {
        return GetValidStatusChanges(status).Contains(newStatus);
    }
}