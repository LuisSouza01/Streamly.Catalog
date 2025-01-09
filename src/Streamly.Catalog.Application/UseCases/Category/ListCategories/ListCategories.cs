using Streamly.Catalog.Application.UseCases.Category.Common;
using Streamly.Catalog.Domain.Repositories;
using Streamly.Catalog.Domain.SeedWork.SearchableRepository;

namespace Streamly.Catalog.Application.UseCases.Category.ListCategories;

public class ListCategories(ICategoryRepository categoryRepository) : IListCategories
{
    public async Task<ListCategoriesOutput> Handle(ListCategoriesInput request, CancellationToken cancellationToken)
    {
        var searchOutput = await categoryRepository.SearchAsync(
            new SearchInput(
                page: request.Page,
                perPage: request.PerPage, 
                search: request.Search, 
                orderBy: request.Sort, 
                order: request.Dir
            ),
            cancellationToken
        );

        return new ListCategoriesOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items
                .Select(CategoryModelOutput.FromCategory)
                .ToList()
        );
    }
}