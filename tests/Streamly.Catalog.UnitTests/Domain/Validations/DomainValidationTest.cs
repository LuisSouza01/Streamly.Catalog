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
    
    [Theory(DisplayName = nameof(ShouldThrowWhenValueSmallerThanMin))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(
        nameof(DomainValidationTestDataGenerator.GetValuesSmallerThanLength), 
        parameters: 10, 
        MemberType = typeof(DomainValidationTestDataGenerator)
    )]
    public void ShouldThrowWhenValueSmallerThanMin(string invalidValue, int minLength)
    {
        #region Arrange
        
            var fieldName = fixture.GetExampleFieldName();

        #endregion

        #region Act

            var action =
                () => DomainValidation.MinLength(invalidValue, minLength, fieldName);

        #endregion

        #region Assert

            action.Should().Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should be at least {minLength} characters long.");

        #endregion
    }
    
    [Theory(DisplayName = nameof(ShouldNotThrowWhenValueGreaterThanMin))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(
        nameof(DomainValidationTestDataGenerator.GetValuesGreaterThanLength), 
        parameters: 10, 
        MemberType = typeof(DomainValidationTestDataGenerator)
    )]
    public void ShouldNotThrowWhenValueGreaterThanMin(string value, int minLength)
    {
        #region Arrange
        
            var fieldName = fixture.GetExampleFieldName();

        #endregion

        #region Act

            var action =
                () => DomainValidation.MinLength(value, minLength, fieldName);

        #endregion

        #region Assert

            action.Should().NotThrow();

        #endregion
    }
    
    [Theory(DisplayName = nameof(ShouldThrowWhenValueGreaterThanMax))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(
        nameof(DomainValidationTestDataGenerator.GetValuesGreaterThanLength), 
        parameters: 10, 
        MemberType = typeof(DomainValidationTestDataGenerator)
    )]
    public void ShouldThrowWhenValueGreaterThanMax(string invalidValue, int maxLength)
    {
        #region Arrange
        
            var fieldName = fixture.GetExampleFieldName();

        #endregion

        #region Act

            var action =
                () => DomainValidation.MaxLength(invalidValue, maxLength, fieldName);

        #endregion

        #region Assert

            action.Should().Throw<EntityValidationException>()
                .WithMessage($"{fieldName} should be less or equal {maxLength} characters long.");

        #endregion
    }
    
    [Theory(DisplayName = nameof(ShouldNotThrowWhenValueGreaterThanMin))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(
        nameof(DomainValidationTestDataGenerator.GetValuesSmallerThanLength), 
        parameters: 10, 
        MemberType = typeof(DomainValidationTestDataGenerator)
    )]
    public void ShouldNotThrowWhenValueSmallerThanMax(string value, int maxLength)
    {
        #region Arrange
        
            var fieldName = fixture.GetExampleFieldName();

        #endregion

        #region Act

            var action =
                () => DomainValidation.MaxLength(value, maxLength, fieldName);

        #endregion

        #region Assert

            action.Should().NotThrow();

        #endregion
    }
}