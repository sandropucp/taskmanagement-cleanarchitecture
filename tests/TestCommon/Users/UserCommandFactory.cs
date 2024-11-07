using TaskManagement.Application.Users.Commands.CreateUser;
using TestCommon.TestConstants;

namespace TestCommon.Comments;

public static class UserCommandFactory
{
    public static CreateUserCommand CreateCreateUserCommand(
        string name = null,
        string email = null,
        string role = null) => new(
            name ?? Constants.User.DefaultName,
            email ?? Constants.User.DefaultEmail,
            role ?? Constants.User.DefaultRole);
}