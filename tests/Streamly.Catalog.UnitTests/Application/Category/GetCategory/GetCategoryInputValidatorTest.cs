using FluentAssertions;
using Streamly.Catalog.Application.UseCases.Category.GetCategory;

namespace Streamly.Catalog.UnitTests.Application.Category.GetCategory;

public class GetCategoryInputValidatorTest
{
    [Fact(DisplayName = nameof(ShouldValidate))]
    [Trait("Application", "GetCategoryInputValidation - UseCases")]
    public void ShouldValidate()
    {
        #region Arrange

            var validInput = new GetCategoryInput(Guid.NewGuid());
                
            var validator = new GetCategoryInputValidator();
            
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
    [Trait("Application", "GetCategoryInputValidation - UseCases")]
    public void ShouldThrowWhenEmptyGuid()
    {
        #region Arrange

            var invalidInput = new GetCategoryInput(Guid.Empty);
                    
            var validator = new GetCategoryInputValidator();
            
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