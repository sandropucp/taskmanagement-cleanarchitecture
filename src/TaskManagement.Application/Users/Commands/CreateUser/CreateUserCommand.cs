using ErrorOr;
using MediatR;
using TaskManagement.Domain.Users;

namespace TaskManagement.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Name, string Email) :
    IRequest<ErrorOr<User>>;
