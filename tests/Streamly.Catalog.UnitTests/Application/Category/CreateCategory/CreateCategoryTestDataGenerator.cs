namespace Streamly.Catalog.UnitTests.Application.Category.CreateCategory;

public static class CreateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs(int numberOfTests)
    {
        var fixture = new CreateCategoryTestFixture();
        
        var invalidInputsList = new List<object[]>();
        
        const int totalInvalidCases = 5;

        for (var i = 0; i < numberOfTests; i++)
        {
            switch (i % totalInvalidCases)
            {
                case 0:
                    invalidInputsList.Add(
                        new object[] {
                            fixture.GetInvalidCategoryInputNameNull(),
                            "Name should not be empty or null."
                        }
                    );
                    break;
                case 1:
                    invalidInputsList.Add(
                        new object[] {
                            fixture.GetInvalidCategoryInputShortName(), 
                            "Name should be at least 3 characters long."
                        }
                    );
                    break;
                case 2:
                    invalidInputsList.Add(
                        new object[] {
                            fixture.GetInvalidCategoryInputTooLongName(), 
                            "Name should be less or equal 255 characters long."
                        }
                    );
                    break;
                case 3:
                    invalidInputsList.Add(
                        new object[] {
                            fixture.GetInvalidCategoryInputDescriptionNull(), 
                            "Description should not be null."
                        }
                    );
                    break;
                case 4:
                    invalidInputsList.Add(
                        new object[] {
                            fixture.GetInvalidCategoryInputTooLongDescription(), 
                            "Description should be less or equal 10000 characters long."
                        }
                    );
                    break;
            }
        }
        
        return invalidInputsList;
    }
}