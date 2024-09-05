using ErrorOr;
using MediatR;
using TaskManagement.Domain.Comments;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Comments.Commands.CreateComment;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, ErrorOr<Comment>>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly ITasksRepository _tasksRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCommentCommandHandler(ICommentsRepository commentsRepository,
        IUsersRepository usersRepository, ITasksRepository tasksRepository, IUnitOfWork unitOfWork)
    {
        _commentsRepository = commentsRepository;
        _usersRepository = usersRepository;
        _tasksRepository = tasksRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Comment>> Handle(CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        var task = await _tasksRepository.GetByIdAsync(request.TaskId);
        if (task is null)
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
            taskId: task.Id,
            userId: user.Id);

        var addCommentResult = task.AddComment(comment);

        if (addCommentResult.IsError)
        {
            return addCommentResult.Errors;
        }

        await _tasksRepository.UpdateTaskAsync(task);               //Task with the comment
        await _commentsRepository.AddCommentAsync(comment);         //Add the comment
        await _unitOfWork.CommitChangesAsync();

        return comment;
    }
}