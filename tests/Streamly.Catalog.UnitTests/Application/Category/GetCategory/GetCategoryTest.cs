using Moq;
using FluentAssertions;
using UseCases = Streamly.Catalog.Application.UseCases.Category.GetCategory;

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
    
    [Fact(DisplayName = nameof(ShouldThrowNotFoundExceptionWhenCategoryDoesNotExists))]
    [Trait("Application", "GetCategory - UseCases")]
    public async Task ShouldThrowNotFoundExceptionWhenCategoryDoesNotExists()
    {
        #region Arrange

        var exampleGuid = Guid.NewGuid();
                
        var repositoryMock = fixture.GetCategoryRepositoryMock();
                
        repositoryMock.Setup(x =>
            x.GetAsync(
                It.IsAny<Guid>(), 
                It.IsAny<CancellationToken>()
            )
        ).ThrowsAsync(new Exceptions.NotFoundException($"Category '{exampleGuid}' not found."));
                
        var input = new UseCases.GetCategoryInput(exampleGuid);

        var useCase = new UseCases.GetCategory(repositoryMock.Object);

        #endregion

        #region Act

        var task = 
            async () => await useCase.Handle(input, CancellationToken.None);

        #endregion

        #region Assert
        
        await task.Should().ThrowAsync<Exceptions.NotFoundException>();

        repositoryMock.Verify(
            repository => repository.GetAsync(
                It.IsAny<Guid>(), 
                It.IsAny<CancellationToken>()
            ), 
            Times.Once
        );

        #endregion
    }
}