using Streamly.Catalog.Application.UseCases.Category.UpdateCategory;
using Streamly.Catalog.UnitTests.Application.Category.Common;

namespace Streamly.Catalog.UnitTests.Application.Category.UpdateCategory;

public class UpdateCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public UpdateCategoryInput GetValidInput(Guid? id = null)
        => new(
            id ?? Guid.NewGuid(),
            GetValidName(),
            GetValidDescription(),
            GetRandomBoolean()
        );
    
    public UpdateCategoryInput GetInvalidCategoryInputShortName()
    {
        var invalidInputShortName = GetValidInput();
        
        invalidInputShortName.Name = invalidInputShortName.Name[..2];

        return invalidInputShortName;
    }
    
    public UpdateCategoryInput GetInvalidCategoryInputTooLongName()
    {
        var invalidInputTooLongName = GetValidInput();

        while (invalidInputTooLongName.Name.Length <= 255)
            invalidInputTooLongName.Name = $"{invalidInputTooLongName.Name} {invalidInputTooLongName.Name}";

        return invalidInputTooLongName;
    }
    
    public UpdateCategoryInput GetInvalidCategoryInputDescriptionNull()
    {
        var invalidDescriptionNull = GetValidInput();

        invalidDescriptionNull.Description = null!;

        return invalidDescriptionNull;
    }
}

[CollectionDefinition(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTestFixtureCollection
    : ICollectionFixture<UpdateCategoryTestFixture> {}