using Moq;
using FluentAssertions;
using Streamly.Catalog.Domain.Exceptions;
using DomainEntity = Streamly.Catalog.Domain.Entities;
using UseCases = Streamly.Catalog.Application.UseCases.Category.CreateCategory;

namespace Streamly.Catalog.UnitTests.Application.Category.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest(CreateCategoryTestFixture fixture)
{
    [Fact(DisplayName = nameof(ShouldCreateCategory))]
    [Trait("Application", "Category - UseCases")]
    public async Task ShouldCreateCategory()
    {
        #region Arrange

            var unitOfWorkMock = fixture.GetUnitOfWorkMock();
            var repositoryMock = fixture.GetCategoryRepositoryMock();
            
            var useCase = new UseCases.CreateCategory(
                unitOfWorkMock.Object,
                repositoryMock.Object
            );
            
            var input = fixture.GetValidCategoryInput();

        #endregion

        #region Act

            var output = await useCase.Handle(input, CancellationToken.None);

        #endregion

        #region Assert

            repositoryMock.Verify(
                repository => repository.InsertAsync(
                    It.IsAny<DomainEntity.Category>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
            
            unitOfWorkMock.Verify(
                uow => uow.CommitAsync(
                   It.IsAny<CancellationToken>() 
                ),
                Times.Once
            );
            
            output.Should().NotBeNull();
            output.Id.Should().NotBeEmpty();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(input.IsActive);
            output.CreatedAt.Should().NotBeSameDateAs(default);

        #endregion
    }
    
    [Fact(DisplayName = nameof(ShouldCreateCategoryWithOnlyName))]
    [Trait("Application", "Category - UseCases")]
    public async Task ShouldCreateCategoryWithOnlyName()
    {
        #region Arrange

            var unitOfWorkMock = fixture.GetUnitOfWorkMock();
            var repositoryMock = fixture.GetCategoryRepositoryMock();
                
            var useCase = new UseCases.CreateCategory(
                unitOfWorkMock.Object,
                repositoryMock.Object
            );

            var input = new UseCases.CreateCategoryInput(fixture.GetValidName());

        #endregion

        #region Act

            var output = await useCase.Handle(input, CancellationToken.None);

        #endregion

        #region Assert

            repositoryMock.Verify(
                repository => repository.InsertAsync(
                    It.IsAny<DomainEntity.Category>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
                
            unitOfWorkMock.Verify(
                uow => uow.CommitAsync(
                    It.IsAny<CancellationToken>() 
                ),
                Times.Once
            );
                
            output.Should().NotBeNull();
            output.Id.Should().NotBeEmpty();
            output.Name.Should().Be(input.Name);
            output.Description.Should().BeEmpty();
            output.IsActive.Should().BeTrue();
            output.CreatedAt.Should().NotBeSameDateAs(default);

        #endregion
    }
    
    [Fact(DisplayName = nameof(ShouldCreateCategoryWithOnlyNameAndDescription))]
    [Trait("Application", "Category - UseCases")]
    public async Task ShouldCreateCategoryWithOnlyNameAndDescription()
    {
        #region Arrange

            var unitOfWorkMock = fixture.GetUnitOfWorkMock();
            var repositoryMock = fixture.GetCategoryRepositoryMock();
                    
            var useCase = new UseCases.CreateCategory(
                unitOfWorkMock.Object,
                repositoryMock.Object
            );

            var input = new UseCases.CreateCategoryInput(
                fixture.GetValidName(),
                fixture.GetValidDescription()
            );

        #endregion

        #region Act

            var output = await useCase.Handle(input, CancellationToken.None);

        #endregion

        #region Assert

            repositoryMock.Verify(
                repository => repository.InsertAsync(
                    It.IsAny<DomainEntity.Category>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
                    
            unitOfWorkMock.Verify(
                uow => uow.CommitAsync(
                    It.IsAny<CancellationToken>() 
                ),
                Times.Once
            );
                    
            output.Should().NotBeNull();
            output.Id.Should().NotBeEmpty();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().BeTrue();
            output.CreatedAt.Should().NotBeSameDateAs(default);

        #endregion
    }

    [Theory(DisplayName = nameof(ShouldThrowWhenTryToCreateCategory))]
    [Trait("Application", "Category - UseCases")]
    [MemberData(
        nameof(CreateCategoryTestDataGenerator.GetInvalidInputs), 
        parameters: 20, 
        MemberType = typeof(CreateCategoryTestDataGenerator)
    )]
    public async Task ShouldThrowWhenTryToCreateCategory(UseCases.CreateCategoryInput invalidInput, string exceptionMessage)
    {
        #region Arrange

            var unitOfWorkMock = fixture.GetUnitOfWorkMock();
            var repositoryMock = fixture.GetCategoryRepositoryMock();
                
            var useCase = new UseCases.CreateCategory(
                unitOfWorkMock.Object,
                repositoryMock.Object
            );

        #endregion

        #region Act

            var task =
                async () => await useCase.Handle(invalidInput, CancellationToken.None);

        #endregion

        #region Assert

            await task.Should().ThrowAsync<EntityValidationException>()
                .WithMessage(exceptionMessage);

        #endregion
    }
}