using MediatR;

namespace Streamly.Catalog.Application.UseCases.Category.GetCategory;

public interface IGetCategory
    : IRequestHandler<GetCategoryInput, GetCategoryOutput> { }