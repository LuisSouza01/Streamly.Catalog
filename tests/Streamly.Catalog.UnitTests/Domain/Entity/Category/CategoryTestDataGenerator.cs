namespace Streamly.Catalog.UnitTests.Domain.Entity.Category;

public static class CategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetRandomBoolean(int numberOfTests)
    {
        var random = new Random();

        for (var i = 0; i < numberOfTests; i++)
        {
            var isActive = random.Next(0, 10) < 5;

            yield return new object[] { isActive };
        }
    }
}