using FluentAssertions;
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
}