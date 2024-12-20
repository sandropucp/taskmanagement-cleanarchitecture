using ErrorOr;

using MediatR;
using TaskManagement.Application.Authentication.Common;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Common.Interfaces;
using TaskManagement.Domain.Users;

namespace TaskManagement.Application.Authentication.Commands.Register;

public class RegisterCommandHandler(
    IJwtTokenGenerator jwtTokenGenerator,
    IPasswordHasher passwordHasher,
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork)
        : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await usersRepository.ExistsByEmailAsync(request.Email))
        {
            return Error.Conflict(description: "User already exists");
        }

        var hashPasswordResult = passwordHasher.HashPassword(request.Password);

        if (hashPasswordResult.IsError)
        {
            return hashPasswordResult.Errors;
        }

        var user = new User(
            request.FirstName,
            request.LastName,
            request.Email,
            hashPasswordResult.Value);

        await usersRepository.AddUserAsync(user);
        await unitOfWork.CommitChangesAsync();

        var token = jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
