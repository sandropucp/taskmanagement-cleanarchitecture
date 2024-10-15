using ErrorOr;
using MediatR;
using TaskManagement.Domain.Users;

namespace TaskManagement.Application.Users.Queries.ListUsers;

public record ListUsersQuery : IRequest<ErrorOr<List<User>>>;
