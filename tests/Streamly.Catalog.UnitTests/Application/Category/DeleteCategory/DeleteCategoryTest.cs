using Moq;
using UseCases = Streamly.Catalog.Application.UseCases.Category.DeleteCategory;

namespace Streamly.Catalog.UnitTests.Application.Category.DeleteCategory;

[Collection(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTest(DeleteCategoryTestFixture fixture)
{
    [Fact(DisplayName = nameof(ShouldDeleteCategory))]
    [Trait("Application", "DeleteCategory - UseCases")]
    public async Task ShouldDeleteCategory()
    {
        #region Arrange

            var repositoryMock = fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = fixture.GetUnitOfWorkMock();
            var validCategory = fixture.GetExampleCategory();

            repositoryMock.Setup(x => x.GetAsync(
                validCategory.Id,
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(validCategory);
                
            var input = new UseCases.DeleteCategoryInput(validCategory.Id);
            var useCase = new UseCases.DeleteCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

        #endregion

        #region Act

            await useCase.Handle(input, CancellationToken.None);

        #endregion

        #region Assert
        
            repositoryMock.Verify(
                repository => repository.GetAsync(
                    validCategory.Id,
                    It.IsAny<CancellationToken>()
                ), 
                Times.Once
            );

            repositoryMock.Verify(
                repository => repository.DeleteAsync(
                    validCategory,
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
                
            unitOfWorkMock.Verify(
                repository => repository.CommitAsync(
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );

        #endregion
    }
}