using Streamly.Catalog.UnitTests.Common;

namespace Streamly.Catalog.UnitTests.Domain.Validations;

public class DomainValidationTestFixture : BaseFixture
{
    public string GetExampleFieldName()
        => Faker.Commerce.ProductName().Replace(" ", "");
    
    public string GetExampleValue()
        => Faker.Commerce.ProductName();
}

[CollectionDefinition(nameof(DomainValidationTestFixture))]
public class DomainValidationTestFixtureCollection
    : ICollectionFixture<DomainValidationTestFixture> {}