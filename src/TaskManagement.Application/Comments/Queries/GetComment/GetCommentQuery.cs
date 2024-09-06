using ErrorOr;
using MediatR;
using TaskManagement.Domain.Comments;

namespace TaskManagement.Application.Comments.Queries.GetComment;

public record GetCommentQuery(Guid CommentId)
    : IRequest<ErrorOr<Comment>>;
