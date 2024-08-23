using ErrorOr;
using MediatR;
using TaskManagement.Domain.Users;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<User>>
{
    private readonly IUsersRepository _usersRepository;

    public CreateUserCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<ErrorOr<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Name, request.Email);
        await _usersRepository.AddUserAsync(user);
        return user;
    }
}