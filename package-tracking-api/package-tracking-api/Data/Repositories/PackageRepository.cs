using Microsoft.EntityFrameworkCore;
using package_tracking_api.Data.Context;
using package_tracking_api.Domain.Dtos;
using package_tracking_api.Domain.Interfaces;
using package_tracking_api.Domain.Models;

namespace package_tracking_api.Data.Repositories;

public class PackageRepository : BaseRepository<Package, PackageDbContext>, IPackageRepository
{
    public PackageRepository(PackageDbContext context) : base(context) { }
    
    public override async Task<List<Package>> GetAllAsync()
    {
        return await Entities
            .Include(p => p.StatusHistory)
            .ToListAsync();
    }
    
    public async Task<Package?> GetByTrackingNumberAsync(string trackingNumber)
    {
        return await Entities
            .Include(p => p.StatusHistory)
            .FirstOrDefaultAsync(p => p.TrackingNumber == trackingNumber);
    }
    
    public async Task<List<Package>> GetFilteredPackagesAsync(PackageFilterDto filters)
    {
        var query = Entities.AsQueryable();

        if (filters.Status.HasValue)
            query = query.Where(p => p.Status == filters.Status.Value);
        
        if (!string.IsNullOrWhiteSpace(filters.SenderName))
            query = query.Where(p => p.Sender.Name.Contains(filters.SenderName));
        
        if (!string.IsNullOrWhiteSpace(filters.RecipientName))
            query = query.Where(p => p.Recipient.Name.Contains(filters.RecipientName));

        return await query.ToListAsync();
    }
}