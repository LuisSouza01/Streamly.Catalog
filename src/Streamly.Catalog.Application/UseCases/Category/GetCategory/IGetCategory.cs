using MediatR;
using Streamly.Catalog.Application.UseCases.Category.Common;

namespace Streamly.Catalog.Application.UseCases.Category.GetCategory;

public interface IGetCategory
    : IRequestHandler<GetCategoryInput, CategoryModelOutput> { }