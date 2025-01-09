using Streamly.Catalog.UnitTests.Application.Category.Common;

namespace Streamly.Catalog.UnitTests.Application.Category.ListCategories;

public class ListCategoriesTestFixture : CategoryUseCasesBaseFixture
{
    
}

[CollectionDefinition(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTestFixtureCollection
    : ICollectionFixture<ListCategoriesTestFixture> {}