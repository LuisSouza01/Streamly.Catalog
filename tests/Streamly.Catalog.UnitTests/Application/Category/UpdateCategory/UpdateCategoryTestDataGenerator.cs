namespace Streamly.Catalog.UnitTests.Application.Category.UpdateCategory;

public static class UpdateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetCategoriesToUpdate(int numberOfTests = 5)
    {
        var fixture = new UpdateCategoryTestFixture();
        
        for (var i = 0; i < numberOfTests; i++)
        {
            var exampleCategory = fixture.GetExampleCategory();

            var input = fixture.GetValidInput(exampleCategory.Id);

            yield return new object[] { exampleCategory, input };
        }
    }
}