using Moq;

namespace Streamly.Catalog.UnitTests.Application.Category.GetCategory;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryTest(GetCategoryTestFixture fixture)
{
    [Fact(DisplayName = nameof(ShouldGetCategory))]
    [Trait("Application", "GetCategory - UseCases")]
    public async Task ShouldGetCategory()
    {
        #region Arrange

            var exampleCategory = fixture.GetExampleCategory();
                
            var repositoryMock = fixture.GetCategoryRepositoryMock();
                
            repositoryMock.Setup(x =>
                x.GetAsync(
                    It.IsAny<Guid>(), 
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(exampleCategory);
                
            var input = new UseCases.GetCategoryInput(exampleCategory.Id);

            var useCase = new UseCases.GetCategory(repositoryMock.Object);

        #endregion

        #region Act

            var output = await useCase.Handle(input, CancellationToken.None);

        #endregion

        #region Assert

            repositoryMock.Verify(
                repository => repository.GetAsync(
                    It.IsAny<Guid>(), 
                    It.IsAny<CancellationToken>()
                ), 
                Times.Once
            );

            output.Should().NotBeNull();
            output.Id.Should().Be(exampleCategory.Id);
            output.Name.Should().Be(exampleCategory.Name);
            output.Description.Should().Be(exampleCategory.Description);
            output.IsActive.Should().Be(exampleCategory.IsActive);
            output.CreatedAt.Should().Be(exampleCategory.CreatedAt);

        #endregion
    }
}