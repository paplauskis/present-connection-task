using package_tracking_api.Domain.Models;

namespace package_tracking_api.Domain.Exceptions;

public class InvalidPackageStatusTransitionException : Exception
{
    public InvalidPackageStatusTransitionException(
        PackageStatus currentStatus, PackageStatus nextStatus)
        : base($"Package status cannot be changed from {currentStatus} to {nextStatus}") {}
}