using MediatR;
using Streamly.Catalog.Application.UseCases.Category.Common;

namespace Streamly.Catalog.Application.UseCases.Category.GetCategory;

public class GetCategoryInput(Guid id) : IRequest<CategoryModelOutput>
{
    public Guid Id { get; private set; } = id;
}