using FluentAssertions;
using Moq;
using Streamly.Catalog.Application.UseCases.Category.Common;
using Streamly.Catalog.Domain.SeedWork.SearchableRepository;
using UseCases = Streamly.Catalog.Application.UseCases.Category.ListCategories;
using DomainEntity = Streamly.Catalog.Domain.Entities;

namespace Streamly.Catalog.UnitTests.Application.Category.ListCategories;

[Collection(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTest(ListCategoriesTestFixture fixture)
{
    [Fact(DisplayName = nameof(ShouldListCorrectly))]
    [Trait("Application", "ListCategories - UseCases")]
    public async Task ShouldListCorrectly()
    {
        #region Arrange

            var repositoryMock = fixture.GetCategoryRepositoryMock();
            
            var input = fixture.GetExampleInput();
            
            var categoriesExampleList = fixture.GetExampleCategoriesList();
            
            var outputRepositorySearch = new SearchOutput<DomainEntity.Category>(
                currentPage: input.Page,
                perPage: input.PerPage,
                items: categoriesExampleList,
                total: new Random().Next(50, 200)
            );
            
            repositoryMock.Setup(repository => 
                repository.SearchAsync(
                    It.Is<SearchInput>(
                        searchInput => searchInput.Page == input.Page
                        && searchInput.PerPage == input.PerPage
                        && searchInput.OrderBy == input.Sort
                        && searchInput.Order == input.Dir
                    ),
                    It.IsAny<CancellationToken>()
                )
            ).ReturnsAsync(outputRepositorySearch);

            var useCase = new UseCases.ListCategories(
                repositoryMock.Object
            );

        #endregion

        #region Act

            var output = await useCase.Handle(input, CancellationToken.None);

        #endregion

        #region Assert

            output.Should().NotBeNull();
            output.Page.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(outputRepositorySearch.Total);
            output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
            ((List<CategoryModelOutput>)output.Items).ForEach(outputItem =>
            {
                var repositoryCategory = outputRepositorySearch.Items.FirstOrDefault(x => x.Id == outputItem.Id);
                    
                outputItem.Should().NotBeNull();
                outputItem.Id.Should().Be(repositoryCategory!.Id);
                outputItem.Name.Should().Be(repositoryCategory.Name);
                outputItem.Description.Should().Be(repositoryCategory.Description);
                outputItem.IsActive.Should().Be(repositoryCategory.IsActive);
                outputItem.CreatedAt.Should().Be(repositoryCategory.CreatedAt);
            });
                
            repositoryMock.Verify(repository => 
                    repository.SearchAsync(
                        It.Is<SearchInput>(
                            searchInput => searchInput.Page == input.Page
                                           && searchInput.PerPage == input.PerPage
                                           && searchInput.OrderBy == input.Sort
                                           && searchInput.Order == input.Dir
                        ),
                        It.IsAny<CancellationToken>()
                    ),
                Times.Once
            );

        #endregion
    }
}