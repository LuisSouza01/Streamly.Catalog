using Bogus;

namespace Streamly.Catalog.UnitTests.Domain.Validations;

public static class DomainValidationTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidValues(int numberOfTests)
    {
        var invalidValues = new[]
        {
            string.Empty,
            null,
            "  "
        };
        
        for (var i = 0; i < numberOfTests; i++)
        {
            var invalidValue = invalidValues[i % invalidValues.Length];

            yield return new object[] { invalidValue! };
        }
    }

    public static IEnumerable<object[]> GetValuesSmallerThanMin(int numberOfTests)
    {
        var faker = new Faker();

        for (var i = 0; i < numberOfTests; i++)
        {
            var value = faker.Commerce.ProductName();

            var minLength = value.Length + new Random().Next(1, 20);

            yield return new object[] { value, minLength };
        }
    }
    
    public static IEnumerable<object[]> GetValuesGreaterThanMin(int numberOfTests)
    {
        var faker = new Faker();

        for (var i = 0; i < numberOfTests; i++)
        {
            var value = faker.Commerce.ProductName();

            var minLength = value.Length - new Random().Next(1, 5);

            yield return new object[] { value, minLength };
        }
    }
}