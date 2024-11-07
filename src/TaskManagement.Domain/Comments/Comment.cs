namespace TaskManagement.Domain.Comments;
public class Comment
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid WorkItemId { get; private set; }
    public string CommentText { get; private set; } = null!;
    public Comment() { }
    public Comment(string commentText, Guid userId, Guid workItemId, Guid? id = null)
    {
        CommentText = commentText;
        UserId = userId;
        WorkItemId = workItemId;
        Id = id ?? Guid.NewGuid();
    }
}
