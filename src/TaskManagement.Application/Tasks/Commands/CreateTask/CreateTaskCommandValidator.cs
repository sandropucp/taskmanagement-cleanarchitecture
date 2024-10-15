using FluentValidation;

namespace TaskManagement.Application.Tasks.Commands.CreateTask;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);
        RuleFor(x => x.Description)
            .MaximumLength(5000);
        RuleFor(x => x.CategoryId)
            .NotEmpty();
        RuleFor(x => x.DueDate)
            .NotEmpty();
    }
}
