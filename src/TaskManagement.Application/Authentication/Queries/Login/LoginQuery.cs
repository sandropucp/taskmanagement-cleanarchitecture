using ErrorOr;

using MediatR;
using TaskManagement.Application.Authentication.Common;

namespace TaskManagement.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
