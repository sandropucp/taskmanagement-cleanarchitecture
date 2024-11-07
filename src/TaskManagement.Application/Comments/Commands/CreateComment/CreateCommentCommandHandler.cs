using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Comments;

namespace TaskManagement.Application.Comments.Commands.CreateComment;

public class CreateCommentCommandHandler(ICommentsRepository commentsRepository,
    IUsersRepository usersRepository, IWorkItemsRepository workItemsRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCommentCommand, ErrorOr<Comment>>
{
    private readonly ICommentsRepository _commentsRepository = commentsRepository;
    private readonly IUsersRepository _usersRepository = usersRepository;
    private readonly IWorkItemsRepository _workItemsRepository = workItemsRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Comment>> Handle(CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var workItem = await _workItemsRepository.GetByIdAsync(request.WorkItemId);
        if (workItem is null)
        {
            return Error.NotFound(description: "Task not found");
        }

        var user = await _usersRepository.GetByIdAsync(request.UserId);
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

        await _workItemsRepository.UpdateWorkItemAsync(workItem);               //Task with the comment
        await _commentsRepository.AddCommentAsync(comment);         //Add the comment
        await _unitOfWork.CommitChangesAsync();

        return comment;
    }
}
