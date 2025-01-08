using MediatR;

namespace Streamly.Catalog.Application.UseCases.Category.DeleteCategory;

public class DeleteCategoryInput(Guid id) : IRequest
{
    public Guid Id { get; private set; } = id;
}