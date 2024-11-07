using FluentValidation;
using TaskManagement.Application.Comments.Commands.CreateComment;

namespace TaskManagement.Application.Users.Commands.CreateTask;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.CommentText)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
        RuleFor(x => x.WorkItemId)
            .NotEmpty();
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}
