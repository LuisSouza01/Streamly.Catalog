using MediatR;

namespace Streamly.Catalog.Application.UseCases.Category.GetCategory;

public class GetCategoryInput(Guid id) : IRequest<GetCategoryOutput>
{
    public Guid Id { get; private set; } = id;
}