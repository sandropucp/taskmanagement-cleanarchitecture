using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.Comments;

public class Comment : Entity
{
    public Guid UserId { get; private set; }
    public Guid WorkItemId { get; private set; }
    public string CommentText { get; private set; } = null!;
    public Comment() { }
    public Comment(
        string commentText,
        Guid userId,
        Guid workItemId,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        CommentText = commentText;
        UserId = userId;
        WorkItemId = workItemId;
        Id = id ?? Guid.NewGuid();
    }
}
