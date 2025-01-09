using Streamly.Catalog.Application.UseCases.Category.ListCategories;

namespace Streamly.Catalog.UnitTests.Application.Category.ListCategories;

public static class ListCategoriesTestDataGenerator
{
    public static IEnumerable<object[]> GetInputsWithoutAllParameters(int numberOfTests)
    {
        var fixture = new ListCategoriesTestFixture();

        var inputExample = fixture.GetExampleInput();

        for (var i = 0; i < numberOfTests; i++)
        {
            yield return (i % 5) switch
            {
                0 => new object[]
                {
                    new ListCategoriesInput()
                },
                1 => new object[]
                {
                    new ListCategoriesInput(
                        page: inputExample.Page
                    )
                },
                2 => new object[]
                {
                    new ListCategoriesInput(
                        page: inputExample.Page, 
                        perPage: inputExample.PerPage
                    )
                },
                3 => new object[]
                {
                    new ListCategoriesInput(
                        page: inputExample.Page, 
                        perPage: inputExample.PerPage,
                        search: inputExample.Search
                    )
                },
                4 => new object[]
                {
                    new ListCategoriesInput(
                        page: inputExample.Page, 
                        perPage: inputExample.PerPage,
                        search: inputExample.Search, 
                        sort: inputExample.Sort
                    )
                },
                5 => new object[]
                {
                    inputExample
                },
                _ => new object[]
                {
                    new ListCategoriesInput()
                }
            };
        }
    }
}