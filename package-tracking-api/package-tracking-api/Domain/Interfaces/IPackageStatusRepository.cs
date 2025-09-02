using package_tracking_api.Domain.Models;

namespace package_tracking_api.Domain.Interfaces;

public interface IPackageStatusRepository : IRepository<PackageStatusHistory> { }