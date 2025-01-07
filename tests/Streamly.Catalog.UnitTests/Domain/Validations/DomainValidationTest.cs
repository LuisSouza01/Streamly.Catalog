using FluentAssertions;
using Streamly.Catalog.Domain.Exceptions;
using Streamly.Catalog.Domain.Validations;

namespace Streamly.Catalog.UnitTests.Domain.Validations;

[Collection(nameof(DomainValidationTestFixture))]
public class DomainValidationTest(DomainValidationTestFixture fixture)
{
    [Fact(DisplayName = nameof(ShouldThrowWhenValueIsNull))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void ShouldThrowWhenValueIsNull()
    {
        #region Arrange

            string? value = null;
            var fieldName = fixture.GetExampleFieldName();

        #endregion

        #region Act

            var action =
                () => DomainValidation.NotNull(value, fieldName);

        #endregion

        #region Assert

            action.Should().Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should not be null.");

        #endregion
    }
    
    [Fact(DisplayName = nameof(ShouldNotThrowWhenValueIsNotNull))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void ShouldNotThrowWhenValueIsNotNull()
    {
        #region Arrange

            var value = fixture.GetExampleValue();
            var fieldName = fixture.GetExampleFieldName();

        #endregion

        #region Act

            var action =
                () => DomainValidation.NotNull(value, fieldName);

        #endregion

        #region Assert

            action.Should().NotThrow();

        #endregion
    }
    
    [Theory(DisplayName = nameof(ShouldThrowWhenValueIsNullOrEmpty))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(
        nameof(DomainValidationTestDataGenerator.GetInvalidValues), 
        parameters: 10, 
        MemberType = typeof(DomainValidationTestDataGenerator)
    )]
    public void ShouldThrowWhenValueIsNullOrEmpty(string? invalidValue)
    {
        #region Arrange
        
            var fieldName = fixture.GetExampleFieldName();

        #endregion

        #region Act

            var action =
                () => DomainValidation.NotNullOrEmpty(invalidValue, fieldName);

        #endregion

        #region Assert

            action.Should().Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should not be empty or null.");

        #endregion
    }
    
    [Fact(DisplayName = nameof(ShouldNotThrowWhenValueIsNotNullOrEmpty))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void ShouldNotThrowWhenValueIsNotNullOrEmpty()
    {
        #region Arrange

            var value = fixture.GetExampleValue();
            var fieldName = fixture.GetExampleFieldName();

        #endregion

        #region Act

            var action =
                () => DomainValidation.NotNullOrEmpty(value, fieldName);

        #endregion

        #region Assert

            action.Should().NotThrow();

        #endregion
    }
}