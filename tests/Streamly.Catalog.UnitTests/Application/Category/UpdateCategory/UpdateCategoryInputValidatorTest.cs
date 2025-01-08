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
    
    [Fact(DisplayName = nameof(ShouldThrowWhenEmptyGuid))]
    [Trait("Application", "UpdateCategoryInputValidator - UseCases")]
    public void ShouldThrowWhenEmptyGuid()
    {
        #region Arrange

            var invalidInput = fixture.GetValidInput(Guid.Empty);
                        
            var validator = new UpdateCategoryInputValidator();
                
        #endregion

        #region Act

            var validationResult = validator.Validate(invalidInput);

        #endregion

        #region Assert

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(1);
            validationResult.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");

        #endregion
    }
}