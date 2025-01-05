using Streamly.Catalog.UnitTests.Common;

namespace Streamly.Catalog.UnitTests.Domain.Entity.Category;

public class CategoryTestFixture : BaseFixture
{
    public Category GetExampleCategory()
        => new(
            Faker.Commerce.ProductName(),
            Faker.Commerce.ProductDescription()
        );
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection 
    : ICollectionFixture<CategoryTestFixture> {}