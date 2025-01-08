using Streamly.Catalog.Application.Interfaces;
using Streamly.Catalog.Domain.Repositories;

namespace Streamly.Catalog.Application.UseCases.Category.DeleteCategory;

public class DeleteCategory(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork)
    : IDeleteCategory
{
    public async Task Handle(DeleteCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetAsync(request.Id, cancellationToken);

        await categoryRepository.DeleteAsync(category, cancellationToken);

        await unitOfWork.CommitAsync(cancellationToken);
    }
}