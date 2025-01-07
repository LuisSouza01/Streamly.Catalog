using Moq;
using FluentAssertions;
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
}