using MediatR;
using Streamly.Catalog.Application.Common;
using Streamly.Catalog.Domain.SeedWork.SearchableRepository;

namespace Streamly.Catalog.Application.UseCases.Category.ListCategories;

public class ListCategoriesInput(int page = 1, int perPage = 15, string search = "", string sort = "", SearchOrder dir = SearchOrder.Asc)
    : PaginatedListInput(page, perPage, search, sort, dir), IRequest<ListCategoriesOutput> { }