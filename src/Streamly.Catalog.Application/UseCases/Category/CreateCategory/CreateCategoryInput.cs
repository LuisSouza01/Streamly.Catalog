using MediatR;

namespace Streamly.Catalog.Application.UseCases.Category.CreateCategory;

public class CreateCategoryInput(
    string name,
    string? description = null,
    bool isActive = true)
    : IRequest<CreateCategoryOutput>
{
    public string Name { get; set; } = name;
    public string? Description { get; set; } = description ?? string.Empty;
    public bool IsActive { get; set; } = isActive;
}