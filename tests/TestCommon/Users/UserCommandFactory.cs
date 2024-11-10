using TaskManagement.Application.Authentication.Commands.Register;
using TestCommon.TestConstants;

namespace TestCommon.Comments;

public static class UserCommandFactory
{
    public static RegisterCommand CreateRegisterCommand(
        string name = null,
        string email = null,
        string role = null,
        string password = null) => new(
            name ?? Constants.User.DefaultName,
            email ?? Constants.User.DefaultEmail,
            role ?? Constants.User.DefaultRole,
            password ?? Constants.User.DefaultPassword);
}