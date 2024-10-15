using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Users;

namespace TaskManagement.Application.Users.Queries.ListUsers;

public class ListUsersQueryHandler(IUsersRepository usersRepository) : IRequestHandler<ListUsersQuery, ErrorOr<List<User>>>
{
    public async Task<ErrorOr<List<User>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await usersRepository.GetAllAsync();
        return users.ToList();
    }
}
