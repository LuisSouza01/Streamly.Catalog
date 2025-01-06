using Streamly.Catalog.UnitTests.Common;
using DomainEntity = Streamly.Catalog.Domain.Entities;

namespace Streamly.Catalog.UnitTests.Domain.Entities.Category;

public class CategoryTestFixture : BaseFixture
{
    public DomainEntity.Category GetExampleCategory()
        => new(
            Faker.Commerce.ProductName(),
            Faker.Commerce.ProductDescription()
        );
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection 
    : ICollectionFixture<CategoryTestFixture> {}