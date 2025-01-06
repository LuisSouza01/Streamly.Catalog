using FluentAssertions;
using Streamly.Catalog.Domain.Exceptions;
using DomainEntity = Streamly.Catalog.Domain.Entities;

namespace Streamly.Catalog.UnitTests.Domain.Entity.Category;

[Collection(nameof(CategoryTestFixture))]
public class CategoryTest(CategoryTestFixture fixture)
{
    [Fact(DisplayName = nameof(ShouldInstantiateCorrectly))]
    [Trait("Domain", "Category - Aggregates")]
    public void ShouldInstantiateCorrectly()
    {
        #region Arrange

            var exampleCategory = fixture.GetExampleCategory();

        #endregion

        #region Act
        
            var dateTimeBefore = DateTime.UtcNow;
            var category = new DomainEntity.Category(
                exampleCategory.Name, 
                exampleCategory.Description
            );
            var dateTimeAfter = DateTime.UtcNow;

        #endregion

        #region Assert

            category.Should().NotBeNull();
            category.Id.Should().NotBeEmpty();
            category.Name.Should().Be(exampleCategory.Name);
            category.Description.Should().Be(exampleCategory.Description);
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            category.CreatedAt.Should().BeOnOrAfter(dateTimeBefore).And.BeOnOrBefore(dateTimeAfter);
            category.IsActive.Should().BeTrue();

        #endregion
    }

    [Theory(DisplayName = nameof(ShouldInstantiateWithIsActiveCorrectly))]
    [Trait("Domain", "Category - Aggregates")]
    [MemberData(nameof(CategoryTestDataGenerator.GetRandomBoolean), parameters: 10, MemberType = typeof(CategoryTestDataGenerator))]
    public void ShouldInstantiateWithIsActiveCorrectly(bool isActive)
    {
        #region Arrange

            var exampleCategory = fixture.GetExampleCategory();

        #endregion

        #region Act

            var dateTimeBefore = DateTime.UtcNow;
            var category = new DomainEntity.Category(
                exampleCategory.Name, 
                exampleCategory.Description,
                isActive
            );
            var dateTimeAfter = DateTime.UtcNow;

        #endregion

        #region Assert

            category.Should().NotBeNull();
            category.Id.Should().NotBeEmpty();
            category.Name.Should().Be(exampleCategory.Name);
            category.Description.Should().Be(exampleCategory.Description);
            category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
            category.CreatedAt.Should().BeOnOrAfter(dateTimeBefore).And.BeOnOrBefore(dateTimeAfter);
            category.IsActive.Should().Be(isActive);

        #endregion
    }

    [Theory(DisplayName = nameof(ShouldThrowWhenNameIsNullOrEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [MemberData(nameof(CategoryTestDataGenerator.GetInvalidCategoryName), parameters: 9, MemberType = typeof(CategoryTestDataGenerator))]
    public void ShouldThrowWhenNameIsNullOrEmpty(string? invalidName)
    {
        #region Arrange

            var exampleCategory = fixture.GetExampleCategory();

        #endregion

        #region Act

            var action =
                () => new DomainEntity.Category(invalidName!, exampleCategory.Description);

        #endregion

        #region Assert

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Name should not be empty or null.");

        #endregion
    }

    [Theory(DisplayName = nameof(ShouldThrowWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [MemberData(nameof(CategoryTestDataGenerator.GetInvalidCategoryNameWithLessThan3Characters), parameters: 10,
        MemberType = typeof(CategoryTestDataGenerator))]
    public void ShouldThrowWhenNameIsLessThan3Characters(string invalidName)
    {
        #region Arrange

            var exampleCategory = fixture.GetExampleCategory();

        #endregion

        #region Act

            var action =
                () => new DomainEntity.Category(invalidName, exampleCategory.Description);

        #endregion

        #region Assert

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Name should be at least 3 characters long.");

        #endregion
    }
    
    [Theory(DisplayName = nameof(ShouldThrowWhenNameIsGraterThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [MemberData(nameof(CategoryTestDataGenerator.GetInvalidCategoryNameWithMoreThan255Characters), parameters: 10,
        MemberType = typeof(CategoryTestDataGenerator))]
    public void ShouldThrowWhenNameIsGraterThan255Characters(string invalidName)
    {
        #region Arrange

            var exampleCategory = fixture.GetExampleCategory();

        #endregion

        #region Act

            var action =
                () => new DomainEntity.Category(invalidName, exampleCategory.Description);

        #endregion

        #region Assert

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Name should be less or equal 255 characters long.");

        #endregion
    }

    [Fact(DisplayName = nameof(ShouldThrowWhenDescriptionIsGreaterThan10_000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void ShouldThrowWhenDescriptionIsGreaterThan10_000Characters()
    {
        #region Arrange

            var exampleCategory = fixture.GetExampleCategory();
            
            var invalidDescription = string.Join(null, Enumerable.Range(1, 10_0001).Select(_ => "a").ToArray());

        #endregion

        #region Act

            var action =
                () => new DomainEntity.Category(exampleCategory.Name, invalidDescription);

        #endregion

        #region Assert

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Description should be less or equal 10000 characters long.");

        #endregion
    }
}