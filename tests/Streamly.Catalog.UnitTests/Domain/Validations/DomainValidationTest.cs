using Streamly.Catalog.Domain.Exceptions;

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
}