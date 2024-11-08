using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Comments;

namespace TaskManagement.Application.Comments.Commands.CreateComment;

public class CreateCommentCommandHandler(ICommentsRepository commentsRepository,
    IUsersRepository usersRepository, IWorkItemsRepository workItemsRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCommentCommand, ErrorOr<Comment>>
{
    public async Task<ErrorOr<Comment>> Handle(CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var workItem = await workItemsRepository.GetByIdAsync(request.WorkItemId);
        if (workItem is null)
        {
            return Error.NotFound(description: "Task not found");
        }

        var user = await usersRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            return Error.NotFound(description: "User not found");
        }

        var comment = new Comment(
            commentText: request.CommentText,
            workItemId: workItem.Id,
            userId: user.Id);

        var addCommentResult = workItem.AddComment(comment);

        if (addCommentResult.IsError)
        {
            return addCommentResult.Errors;
        }

        await workItemsRepository.UpdateWorkItemAsync(workItem);               //Task with the comment
        await commentsRepository.AddCommentAsync(comment);         //Add the comment
        await unitOfWork.CommitChangesAsync();

        return comment;
    }
}
