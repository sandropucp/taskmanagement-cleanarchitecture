using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Comments;

namespace TaskManagement.Application.Comments.Queries.GetComment;

public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, ErrorOr<Comment>>
{
    private readonly ICommentsRepository _commentsRepository;

    public GetCommentQueryHandler(ICommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }

    public async Task<ErrorOr<Comment>> Handle(GetCommentQuery request, CancellationToken cancellationToken)
    {
        var comment = await _commentsRepository.GetByIdAsync(request.CommentId);

        return comment == null
            ? Error.NotFound(description: "Comment not found")
            : comment;
    }
}