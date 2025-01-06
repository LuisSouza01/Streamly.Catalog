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
}