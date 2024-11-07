using TaskManagement.Domain.Users;
using TestCommon.TestConstants;

namespace TestCommon.Comments;

public static class UserFactory
{
    public static User CreateUser(
        Guid? id = null,
        string name = null,
        string email = null,
        string role = null) => new(
            name ?? Constants.User.DefaultName,
            email ?? Constants.User.DefaultEmail,
            role ?? Constants.User.DefaultRole,
            id ?? Constants.User.Id);
}
