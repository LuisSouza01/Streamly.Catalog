using Bogus;

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

    public static IEnumerable<object[]> GetInvalidCategoryName(int numberOfTests)
    {
        var invalidValues = new[]
        {
            string.Empty,
            null,
            "  "
        };
        
        for (var i = 0; i < numberOfTests; i++)
        {
            var invalidName = invalidValues[i % invalidValues.Length];

            yield return new object[] { invalidName! };
        }
    }

    public static IEnumerable<object[]> GetInvalidCategoryWithLessThan3Characters(int numberOfTests)
    {
        var faker = new Faker();

        for (var i = 0; i < numberOfTests; i++)
        {
            var isOdd = i % 2 == 1;

            yield return new object[] { faker.Commerce.ProductName()[..(isOdd ? 1 : 2)] };
        }
    }
}