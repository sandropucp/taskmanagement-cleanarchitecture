using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Comments;

namespace TaskManagement.Application.Comments.Queries.GetComment;

public class GetCommentQueryHandler(ICommentsRepository commentsRepository) : IRequestHandler<GetCommentQuery, ErrorOr<Comment>>
{
    private readonly ICommentsRepository commentsRepository = commentsRepository;

    public async Task<ErrorOr<Comment>> Handle(GetCommentQuery request, CancellationToken cancellationToken)
    {
        var comment = await commentsRepository.GetByIdAsync(request.CommentId);

        return comment == null
            ? Error.NotFound(description: "Comment not found")
            : comment;
    }
}
