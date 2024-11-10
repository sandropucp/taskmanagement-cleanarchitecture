using ErrorOr;

using MediatR;
using TaskManagement.Application.Authentication.Common;

namespace TaskManagement.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;