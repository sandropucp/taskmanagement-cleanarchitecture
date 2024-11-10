using FluentValidation;
using TaskManagement.Application.Authentication.Commands.Register;

namespace TaskManagement.Application.Users.Commands.CreateTask;

public class CreateUserCommandValidator : AbstractValidator<RegisterCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);
        RuleFor(x => x.Email)
            .NotEmpty();
    }
}
