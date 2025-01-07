using Streamly.Catalog.Application.Interfaces;
using Streamly.Catalog.Domain.Repositories;
using DomainEntity = Streamly.Catalog.Domain.Entities;

namespace Streamly.Catalog.Application.UseCases.Category.CreateCategory;

public class CreateCategory(
    IUnitOfWork unitOfWork, 
    ICategoryRepository categoryRepository) : ICreateCategory
{
    public async Task<CreateCategoryOutput> Handle(CreateCategoryInput request, CancellationToken cancellationToken)
    {
        var category = new DomainEntity.Category(
            request.Name,
            request.Description!,
            request.IsActive
        );

        await categoryRepository.InsertAsync(category, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return CreateCategoryOutput
            .FromCategory(category);
    }
}