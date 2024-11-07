using FluentValidation;

namespace TaskManagement.Application.WorkItems.Commands.CreateWorkItem;

public class CreateWorkItemCommandValidator : AbstractValidator<CreateWorkItemCommand>
{
    public CreateWorkItemCommandValidator()
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
