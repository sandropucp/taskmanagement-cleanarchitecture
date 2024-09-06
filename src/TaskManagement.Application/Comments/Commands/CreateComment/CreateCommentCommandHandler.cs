using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Comments;

namespace TaskManagement.Application.Comments.Commands.CreateComment;

public class CreateCommentCommandHandler(ICommentsRepository commentsRepository,
    IUsersRepository usersRepository, ITasksRepository tasksRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateCommentCommand, ErrorOr<Comment>>
{
    private readonly ICommentsRepository commentsRepository = commentsRepository;
    private readonly IUsersRepository usersRepository = usersRepository;
    private readonly ITasksRepository tasksRepository = tasksRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<ErrorOr<Comment>> Handle(CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var task = await tasksRepository.GetByIdAsync(request.TaskId);
        if (task is null)
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
            taskId: task.Id,
            userId: user.Id);

        var addCommentResult = task.AddComment(comment);

        if (addCommentResult.IsError)
        {
            return addCommentResult.Errors;
        }

        await tasksRepository.UpdateTaskAsync(task);               //Task with the comment
        await commentsRepository.AddCommentAsync(comment);         //Add the comment
        await unitOfWork.CommitChangesAsync();

        return comment;
    }
}
