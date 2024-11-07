using TaskManagement.Application.Categories.Commands.CreateCategory;
using TestCommon.TestConstants;

namespace TestCommon.Categories;

public static class CategoryCommandFactory
{
    public static CreateCategoryCommand CreateCreateCategoryCommand(
        string name = null) => new(
            name ?? Constants.Category.DefaultName);
}
