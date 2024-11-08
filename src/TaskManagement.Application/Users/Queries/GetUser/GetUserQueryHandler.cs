using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Users;

namespace TaskManagement.Application.Users.Queries.GetUser;

public class GetUserQueryHandler(IUsersRepository usersRepository) : IRequestHandler<GetUserQuery, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetByIdAsync(request.UserId);
        return user is null
            ? Error.NotFound(description: "User not found")
            : user;
    }
}
