using Microsoft.EntityFrameworkCore;
using package_tracking_api.Data.Context;

namespace Tests.Utilities;

public static class DbContextGenerator
{
    public static PackageDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<PackageDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new PackageDbContext(options);
    }
}