using Moq;
using Streamly.Catalog.Application.Interfaces;
using Streamly.Catalog.Application.UseCases.Category.CreateCategory;
using Streamly.Catalog.UnitTests.Common;
using Streamly.Catalog.Domain.Repositories;

namespace Streamly.Catalog.UnitTests.Application.Category.CreateCategory;

public class CreateCategoryTestFixture : BaseFixture
{
    public Mock<IUnitOfWork> GetUnitOfWorkMock()
        => new();
    
    public Mock<ICategoryRepository> GetCategoryRepositoryMock()
        => new();
    
    public CreateCategoryInput GetValidCategoryInput()
        => new(
            GetValidName(),
            GetValidDescription(),
            GetRandomBoolean()
        );
    
    public string GetValidName()
    {
        var categoryName = string.Empty;

        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];

        if (categoryName.Length > 255)
            categoryName = categoryName[..255];

        return categoryName;
    }

    public string GetValidDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();

        if (categoryDescription.Length > 10_000)
            categoryDescription = categoryDescription[..10_000];

        return categoryDescription;
    }

    private bool GetRandomBoolean()
        => new Random().NextDouble() < 0.5;
    
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