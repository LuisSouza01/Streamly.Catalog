using FluentAssertions;
using Moq;
using Streamly.Catalog.Application.Exceptions;
using Streamly.Catalog.Domain.Exceptions;
using DomainEntity = Streamly.Catalog.Domain.Entities;
using UseCases = Streamly.Catalog.Application.UseCases.Category.UpdateCategory;

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

    [Fact(DisplayName = nameof(ShouldThrowWhenCategoryNotFound))]
    [Trait("Application", "UpdateCategory - UseCases")]
    public async Task ShouldThrowWhenCategoryNotFound()
    {
        #region Arrange

            var repositoryMock = fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = fixture.GetUnitOfWorkMock();
            var input = fixture.GetValidInput();
                    
            repositoryMock.Setup(x => x.GetAsync(
                input.Id,
                It.IsAny<CancellationToken>()
            )).ThrowsAsync(new NotFoundException($"Category '{input.Id} not found.'"));
                    
            var useCase = new UseCases.UpdateCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

        #endregion

        #region Act

            var task = 
                async () => await useCase.Handle(input, CancellationToken.None);

        #endregion

        #region Assert

            await task.Should().ThrowAsync<NotFoundException>();
            
            repositoryMock.Verify(repository => 
                    repository.GetAsync(
                        input.Id,
                        It.IsAny<CancellationToken>()
                    ),
                Times.Once
            );

        #endregion
    }
    
    [Theory(DisplayName = nameof(ShouldUpdateCategoryWithoutIsActive))]
    [Trait("Application", "UpdateCategory - UseCases")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate), 
        parameters: 10, 
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task ShouldUpdateCategoryWithoutIsActive(DomainEntity.Category validCategory, UseCases.UpdateCategoryInput exampleInput)
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
            
            var input = new UseCases.UpdateCategoryInput(
                exampleInput.Id,
                exampleInput.Name,
                exampleInput.Description
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
            output.IsActive.Should().Be(validCategory.IsActive);

        #endregion
    }
    
    [Theory(DisplayName = nameof(ShouldUpdateCategoryOnlyWithName))]
    [Trait("Application", "UpdateCategory - UseCases")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetCategoriesToUpdate), 
        parameters: 10, 
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task ShouldUpdateCategoryOnlyWithName(DomainEntity.Category validCategory, UseCases.UpdateCategoryInput exampleInput)
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
            
            var input = new UseCases.UpdateCategoryInput(
                exampleInput.Id,
                exampleInput.Name
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
            output.Description.Should().Be(validCategory.Description);
            output.IsActive.Should().Be(validCategory.IsActive);

        #endregion
    }

    [Theory(DisplayName = nameof(ShouldThrowWhenCanUpdateCategory))]
    [Trait("Application", "UpdateCategory - UseCases")]
    [MemberData(
        nameof(UpdateCategoryTestDataGenerator.GetInvalidInputs),
        parameters: 10,
        MemberType = typeof(UpdateCategoryTestDataGenerator)
    )]
    public async Task ShouldThrowWhenCanUpdateCategory(UseCases.UpdateCategoryInput invalidInput, string exceptionMessage)
    {
        #region Arrange

            var exampleCategory = fixture.GetExampleCategory();
            
            invalidInput.Id = exampleCategory.Id;
            
            var unitOfWorkMock = fixture.GetUnitOfWorkMock();
            var repositoryMock = fixture.GetCategoryRepositoryMock();
                
            repositoryMock.Setup(x => x.GetAsync(
                exampleCategory.Id, 
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(exampleCategory);
                    
            var useCase = new UseCases.UpdateCategory(
                repositoryMock.Object,
                unitOfWorkMock.Object
            );

        #endregion

        #region Act

            var task =
                async () => await useCase.Handle(invalidInput, CancellationToken.None);

        #endregion

        #region Assert

            await task.Should().ThrowAsync<EntityValidationException>()
                .WithMessage(exceptionMessage);
            
            repositoryMock.Verify(
                repository => repository.GetAsync(
                    exampleCategory.Id, 
                    It.IsAny<CancellationToken>()
                ), 
                Times.Once
            );

        #endregion
    }
}