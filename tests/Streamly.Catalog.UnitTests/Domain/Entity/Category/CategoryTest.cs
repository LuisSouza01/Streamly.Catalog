namespace Streamly.Catalog.UnitTests.Domain.Entity.Category;

[Collection(nameof(CategoryTestFixture))]
public class CategoryTest(CategoryTestFixture fixture)
{
    private readonly CategoryTestFixture _fixture = fixture;
}