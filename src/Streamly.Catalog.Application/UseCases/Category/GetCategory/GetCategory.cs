using Streamly.Catalog.Application.UseCases.Category.Common;
using Streamly.Catalog.Domain.Repositories;

namespace Streamly.Catalog.Application.UseCases.Category.GetCategory;

public class GetCategory(ICategoryRepository categoryRepository) : IGetCategory
{
    public async Task<CategoryModelOutput> Handle(GetCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetAsync(request.Id, cancellationToken);
        
        return CategoryModelOutput
            .FromCategory(category);
    }
}