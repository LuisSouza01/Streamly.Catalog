using Streamly.Catalog.Domain.Repositories;
using Streamly.Catalog.Application.Interfaces;

namespace Streamly.Catalog.Application.UseCases.Category.UpdateCategory;

public class UpdateCategory(
    ICategoryRepository categoryRepository, 
    IUnitOfWork unitOfWork) : IUpdateCategory
{
    public async Task<UpdateCategoryOutput> Handle(UpdateCategoryInput request, CancellationToken cancellationToken)
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

        return UpdateCategoryOutput
            .FromCategory(category);
    }
}