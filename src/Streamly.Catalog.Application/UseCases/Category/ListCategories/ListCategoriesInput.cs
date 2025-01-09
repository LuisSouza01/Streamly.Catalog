using MediatR;
using Streamly.Catalog.Application.Common;
using Streamly.Catalog.Domain.SeedWork.SearchableRepository;

namespace Streamly.Catalog.Application.UseCases.Category.ListCategories;

public class ListCategoriesInput(int page, int perPage, string search, string sort, SearchOrder dir)
    : PaginatedListInput(page, perPage, search, sort, dir), IRequest<ListCategoriesOutput> { }