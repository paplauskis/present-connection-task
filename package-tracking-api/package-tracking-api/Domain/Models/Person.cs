using Microsoft.EntityFrameworkCore;

namespace package_tracking_api.Domain.Models;

[Owned]
public class Person
{
    public required string Name { get; set; }
    
    public required string Address { get; set; }
    
    public required string Phone { get; set; }
}