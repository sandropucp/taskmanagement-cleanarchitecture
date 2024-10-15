using ErrorOr;
using MediatR;

namespace TaskManagement.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : IRequest<ErrorOr<Deleted>>;
