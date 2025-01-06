using Streamly.Catalog.Domain.Exceptions;
using Streamly.Catalog.Domain.SeedWork;

namespace Streamly.Catalog.Domain.Entities;

public class Category : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public Category(string name, string description, bool isActive = true)
    {
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.UtcNow;
        
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new EntityValidationException("Name should not be empty or null.");
        
        if (Name.Length < 3)
            throw new EntityValidationException("Name should be at least 3 characters long.");
        
        if (Name.Length > 255)
            throw new EntityValidationException("Name should be less or equal 255 characters long.");
        
        if (Description.Length > 10_000)
            throw new EntityValidationException("Description should be less or equal 10000 characters long.");
    }
}