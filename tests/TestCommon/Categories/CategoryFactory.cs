using TaskManagement.Domain.Categories;
using TestCommon.TestConstants;

namespace TestCommon.Comments;

public static class CategoryFactory
{
    public static Category CreateCategory(
        Guid? id = null,
        string name = null) => new(
            name ?? Constants.Category.DefaultName,
            id ?? Constants.Category.Id);
}
