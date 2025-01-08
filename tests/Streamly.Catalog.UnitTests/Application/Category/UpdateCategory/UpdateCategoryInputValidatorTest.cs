using FluentAssertions;
using Streamly.Catalog.Application.UseCases.Category.UpdateCategory;

namespace Streamly.Catalog.UnitTests.Application.Category.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryInputValidatorTest(
    UpdateCategoryTestFixture fixture)
{
    [Fact(DisplayName = nameof(ShouldValidate))]
    [Trait("Application", "UpdateCategoryInputValidator - UseCases")]
    public void ShouldValidate()
    {
        #region Arrange

            var validInput = fixture.GetValidInput();
                    
            var validator = new UpdateCategoryInputValidator();
            
        #endregion

        #region Act

            var validationResult = validator.Validate(validInput);

        #endregion

        #region Assert

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().HaveCount(0);

        #endregion
    }
}