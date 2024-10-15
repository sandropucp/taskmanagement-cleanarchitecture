using FluentValidation;
using TaskManagement.Application.Users.Commands.CreateUser;

namespace TaskManagement.Application.Users.Commands.CreateTask;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);
        RuleFor(x => x.Email)
            .NotEmpty();
    }
}
