using TaskManagement.Domain.Comments;

namespace TaskManagement.Application.Common.Interfaces;

public interface ICommentsRepository
{
    Task AddCommentAsync(Comment comment);
    Task<Comment?> GetByIdAsync(Guid commentId);
    Task<List<Comment>> GetAllAsync();
}
