using Streamly.Catalog.UnitTests.Application.Category.Common;

namespace Streamly.Catalog.UnitTests.Application.Category.GetCategory;

public class GetCategoryTestFixture 
    : CategoryUseCasesBaseFixture { }

[CollectionDefinition(nameof(GetCategoryTestFixture))]
public class GetCategoryTestFixtureCollection
    : ICollectionFixture<GetCategoryTestFixture> { }