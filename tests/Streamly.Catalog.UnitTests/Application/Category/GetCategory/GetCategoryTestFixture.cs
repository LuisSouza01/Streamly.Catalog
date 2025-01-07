using Streamly.Catalog.UnitTests.Common;
using DomainEntity = Streamly.Catalog.Domain.Entities;

namespace Streamly.Catalog.UnitTests.Application.Category.GetCategory;

public class GetCategoryTestFixture : BaseFixture
{
    public DomainEntity.Category GetExampleCategory()
        => new(
            GetValidName(),
            GetValidDescription(),
            GetRandomBoolean()
        );
    
    public string GetValidName()
    {
        var categoryName = string.Empty;

        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];

        if (categoryName.Length > 255)
            categoryName = categoryName[..255];

        return categoryName;
    }

    public string GetValidDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();

        if (categoryDescription.Length > 10_000)
            categoryDescription = categoryDescription[..10_000];

        return categoryDescription;
    }

    private bool GetRandomBoolean()
        => new Random().NextDouble() < 0.5;
}

[CollectionDefinition(nameof(GetCategoryTestFixture))]
public class GetCategoryTestFixtureCollection
    : ICollectionFixture<GetCategoryTestFixture> { }