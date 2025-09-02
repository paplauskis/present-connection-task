using System.ComponentModel.DataAnnotations;

namespace package_tracking_api.Domain.Models;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}