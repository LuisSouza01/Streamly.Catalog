using Streamly.Catalog.Domain.SeedWork;

namespace Streamly.Catalog.Domain.Entities;

public class Category(
    string name, 
    string description, 
    bool isActive = true) : AggregateRoot
{
    public string Name { get; } = name;
    public string Description { get; } = description;
    public bool IsActive { get; } = isActive;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}