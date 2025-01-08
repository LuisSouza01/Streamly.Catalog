using Streamly.Catalog.Application.UseCases.Category.CreateCategory;
using Streamly.Catalog.UnitTests.Application.Category.Common;

namespace Streamly.Catalog.UnitTests.Application.Category.CreateCategory;

public class CreateCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public CreateCategoryInput GetValidCategoryInput()
        => new(
            GetValidName(),
            GetValidDescription(),
            GetRandomBoolean()
        );
    
    public CreateCategoryInput GetInvalidCategoryInputShortName()
    {
        var invalidInputShortName = GetValidCategoryInput();

        invalidInputShortName.Name = invalidInputShortName.Name[..2];

        return invalidInputShortName;
    }

    public CreateCategoryInput GetInvalidCategoryInputTooLongName()
    {
        var invalidInputTooLongName = GetValidCategoryInput();

        while (invalidInputTooLongName.Name.Length <= 255)
            invalidInputTooLongName.Name = $"{invalidInputTooLongName.Name} {invalidInputTooLongName.Name}";

        return invalidInputTooLongName;
    }

    public CreateCategoryInput GetInvalidCategoryInputNameNull()
    {
        var invalidNameNull = GetValidCategoryInput();

        invalidNameNull.Name = null!;

        return invalidNameNull;
    }

    public CreateCategoryInput GetInvalidCategoryInputDescriptionNull()
    {
        var invalidDescriptionNull = GetValidCategoryInput();

        invalidDescriptionNull.Description = null!;

        return invalidDescriptionNull;
    }

    public CreateCategoryInput GetInvalidCategoryInputTooLongDescription()
    {
        var invalidInputTooLongDescription = GetValidCategoryInput();

        var tooLongDescriptionForCategory = Faker.Commerce.ProductDescription();

        while (tooLongDescriptionForCategory.Length <= 10_000)
            tooLongDescriptionForCategory = $"{tooLongDescriptionForCategory} {tooLongDescriptionForCategory}";

        invalidInputTooLongDescription.Description = tooLongDescriptionForCategory;

        return invalidInputTooLongDescription;
    }
}

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection
    : ICollectionFixture<CreateCategoryTestFixture> {}