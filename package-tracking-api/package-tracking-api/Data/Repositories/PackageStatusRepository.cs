using package_tracking_api.Data.Context;
using package_tracking_api.Domain.Interfaces;
using package_tracking_api.Domain.Models;

namespace package_tracking_api.Data.Repositories;

public class PackageStatusRepository : BaseRepository<PackageStatusHistory, PackageDbContext>, IPackageStatusRepository
{
    public PackageStatusRepository(PackageDbContext context) : base(context) { }
}