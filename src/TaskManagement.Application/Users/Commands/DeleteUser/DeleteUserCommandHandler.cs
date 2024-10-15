using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(
    IUnitOfWork unitOfWork,
    IUsersRepository usersRepository) : IRequestHandler<DeleteUserCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "category not 1 found");
        }

        await usersRepository.RemoveUserAsync(user);
        await unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}
