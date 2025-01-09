using Streamly.Catalog.Application.Common;
using Streamly.Catalog.Application.UseCases.Category.Common;

namespace Streamly.Catalog.Application.UseCases.Category.ListCategories;

public class ListCategoriesOutput(int page, int perPage, int total, IReadOnlyList<CategoryModelOutput> items) 
    : PaginatedListOutput<CategoryModelOutput>(page, perPage, total, items) { }