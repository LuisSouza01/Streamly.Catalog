using Moq;
using DomainEntity = Streamly.Catalog.Domain.Entities;

namespace Streamly.Catalog.UnitTests.Application.Category.UpdateCategory;

[Collection(nameof(UpdateCategoryTestFixture))]
public class UpdateCategoryTest(UpdateCategoryTestFixture fixture)
{
    [Theory(DisplayName = nameof(ShouldUpdateCategory))]
    [Trait("Application", "UpdateCategory - UseCases")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate), 
        parameters: 10, 
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task ShouldUpdateCategory(DomainEntity.Category validCategory, UseCases.UpdateCategoryInput input)
    {
        #region Arrange

            var repositoryMock = fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = fixture.GetUnitOfWorkMock();
                
            repositoryMock.Setup(x => x.GetAsync(
                validCategory.Id, 
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(validCategory);
                
            var useCase = new UseCases.UpdateCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

        #endregion

        #region Act

            var output = await useCase.Handle(input, CancellationToken.None);

        #endregion

        #region Assert

            repositoryMock.Verify(repository => 
                    repository.GetAsync(
                        validCategory.Id,
                        It.IsAny<CancellationToken>()
                    ),
                Times.Once
            );
                
            repositoryMock.Verify(repository => 
                    repository.UpdateAsync(
                        validCategory,
                        It.IsAny<CancellationToken>()
                    ),
                Times.Once
            );
                
            unitOfWorkMock.Verify(repository => 
                    repository.CommitAsync(
                        It.IsAny<CancellationToken>()
                    ),
                Times.Once
            );

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be((bool)input.IsActive!);

        #endregion
    }
}