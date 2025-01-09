using Streamly.Catalog.Domain.Repositories;
using Streamly.Catalog.Application.Interfaces;
using Streamly.Catalog.Application.UseCases.Category.Common;

namespace Streamly.Catalog.Application.UseCases.Category.UpdateCategory;

public class UpdateCategory(
    ICategoryRepository categoryRepository, 
    IUnitOfWork unitOfWork) : IUpdateCategory
{
    public async Task<CategoryModelOutput> Handle(UpdateCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetAsync(request.Id, cancellationToken);
        
        category.Update(
            request.Name, 
            request.Description
        );

        if (request.IsActive != category.IsActive)
        {
            switch (request.IsActive)
            {
                case true:
                    category.Activate();
                    break;
                case false:
                    category.Deactivate();
                    break;
            }
        }

        await categoryRepository.UpdateAsync(category, cancellationToken);
        
        await unitOfWork.CommitAsync(cancellationToken);

        return CategoryModelOutput
            .FromCategory(category);
    }
}