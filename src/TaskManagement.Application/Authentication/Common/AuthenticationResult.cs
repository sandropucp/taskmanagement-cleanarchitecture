using TaskManagement.Domain.Users;

namespace TaskManagement.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);