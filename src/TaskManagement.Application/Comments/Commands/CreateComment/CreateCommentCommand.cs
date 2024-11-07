using ErrorOr;
using MediatR;
using TaskManagement.Domain.Comments;

namespace TaskManagement.Application.Comments.Commands.CreateComment;

public record CreateCommentCommand(Guid WorkItemId,
    Guid UserId,
    string CommentText) :
    IRequest<ErrorOr<Comment>>;
