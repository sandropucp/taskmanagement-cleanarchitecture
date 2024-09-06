using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Users;

namespace TaskManagement.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUsersRepository usersRepository) : IRequestHandler<CreateUserCommand, ErrorOr<User>>
{
    private readonly IUsersRepository usersRepository = usersRepository;

    public async Task<ErrorOr<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Name, request.Email);
        await usersRepository.AddUserAsync(user);
        return user;
    }
}
