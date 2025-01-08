namespace Streamly.Catalog.UnitTests.Application.Category.UpdateCategory;

public static class UpdateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetCategoriesToUpdate(int numberOfTests = 5)
    {
        var fixture = new UpdateCategoryTestFixture();
        
        for (var i = 0; i < numberOfTests; i++)
        {
            var exampleCategory = fixture.GetExampleCategory();

            var input = fixture.GetValidInput(exampleCategory.Id);

            yield return new object[] { exampleCategory, input };
        }
    }
    
    public static IEnumerable<object[]> GetInvalidInputs(int numberOfTests)
    {
        var fixture = new UpdateCategoryTestFixture();
        
        var invalidInputsList = new List<object[]>();
        
        const int totalInvalidCases = 3;

        for (var i = 0; i < numberOfTests; i++)
        {
            switch (i % totalInvalidCases)
            {
                case 0:
                    invalidInputsList.Add(
                        new object[] {
                            fixture.GetInvalidCategoryInputShortName(), 
                            "Name should be at least 3 characters long."
                        }
                    );
                    break;
                case 1:
                    invalidInputsList.Add(
                        new object[] {
                            fixture.GetInvalidCategoryInputTooLongName(), 
                            "Name should be less or equal 255 characters long."
                        }
                    );
                    return invalidInputsList;
                case 2:
                    invalidInputsList.Add(
                        new object[] {
                            fixture.GetInvalidCategoryInputDescriptionNull(), 
                            "Description should not be null."
                        }
                    );
                    break;
            }
        }
        
        return invalidInputsList;
    }
}