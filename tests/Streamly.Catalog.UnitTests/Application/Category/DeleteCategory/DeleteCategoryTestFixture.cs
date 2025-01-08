using Moq;
using Streamly.Catalog.Application.Interfaces;
using Streamly.Catalog.Domain.Repositories;
using Streamly.Catalog.UnitTests.Common;
using DomainEntity = Streamly.Catalog.Domain.Entities;

namespace Streamly.Catalog.UnitTests.Application.Category.DeleteCategory;

public class DeleteCategoryTestFixture : BaseFixture
{
    public Mock<IUnitOfWork> GetUnitOfWorkMock()
        => new();
    
    public Mock<ICategoryRepository> GetCategoryRepositoryMock()
        => new();
    
    public DomainEntity.Category GetExampleCategory()
        => new(
            GetValidName(),
            GetValidDescription(),
            GetRandomBoolean()
        );
    
    private string GetValidName()
    {
        var categoryName = string.Empty;

        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];

        if (categoryName.Length > 255)
            categoryName = categoryName[..255];

        return categoryName;
    }

    private string GetValidDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();

        if (categoryDescription.Length > 10_000)
            categoryDescription = categoryDescription[..10_000];

        return categoryDescription;
    }

    private bool GetRandomBoolean()
        => new Random().NextDouble() < 0.5;
}

[CollectionDefinition(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTestFixtureCollection
    : ICollectionFixture<DeleteCategoryTestFixture> {}