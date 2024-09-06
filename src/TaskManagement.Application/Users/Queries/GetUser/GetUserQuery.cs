using ErrorOr;
using MediatR;
using TaskManagement.Domain.Users;

namespace TaskManagement.Application.Users.Queries.GetUser;

public record GetUserQuery(Guid UserId) : IRequest<ErrorOr<User>>;
