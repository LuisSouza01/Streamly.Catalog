using DomainEntity = Streamly.Catalog.Domain.Entities;

namespace Streamly.Catalog.Application.UseCases.Category.GetCategory;

public class GetCategoryOutput(Guid id,
    string name,
    string description,
    bool isActive,
    DateTime createdAt)
{
    public Guid Id { get; private set; } = id;
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public bool IsActive { get; private set; } = isActive;
    public DateTime CreatedAt { get; private set; } = createdAt;

    public static GetCategoryOutput FromCategory(DomainEntity.Category category)
        => new (
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.CreatedAt
        );
}