using Streamly.Catalog.UnitTests.Application.Category.Common;


namespace Streamly.Catalog.UnitTests.Application.Category.DeleteCategory;

public class DeleteCategoryTestFixture 
    : CategoryUseCasesBaseFixture { }

[CollectionDefinition(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTestFixtureCollection
    : ICollectionFixture<DeleteCategoryTestFixture> {}