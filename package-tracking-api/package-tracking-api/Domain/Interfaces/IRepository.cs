using package_tracking_api.Domain.Models;

namespace package_tracking_api.Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T updatedEntity);
    Task DeleteAsync(T entity);
}