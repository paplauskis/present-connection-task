using Microsoft.EntityFrameworkCore;
using package_tracking_api.Domain.Interfaces;
using package_tracking_api.Domain.Models;

namespace package_tracking_api.Data.Repositories;

public abstract class BaseRepository<T, TContext> : IRepository<T>
    where T : BaseEntity
    where TContext : DbContext
{
    protected readonly TContext Context;
    protected DbSet<T> Entities;

    protected BaseRepository(TContext context)
    {
        Context = context;
        Entities = Context.Set<T>();
    }

    public virtual async Task<List<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await Context.Set<T>().FindAsync(id);
    }
    
    public virtual async Task<T> AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
        
        return entity;
    }
    
    public virtual async Task<T> UpdateAsync(T entity)
    {
        Context.Update(entity);
        await Context.SaveChangesAsync();
            
        return entity;
    }
    
    public virtual async Task DeleteAsync(T entity)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
    }
}