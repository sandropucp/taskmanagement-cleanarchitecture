namespace TaskManagement.Domain.Comments;
public class Comment
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid TaskId { get; private set; }
    public string CommentText { get; private set; } = null!;
    public Comment() { }
    public Comment(string commentText, Guid userId, Guid taskId, Guid? id = null)
    {
        CommentText = commentText;
        UserId = userId;
        TaskId = taskId;
        Id = id ?? Guid.NewGuid();
    }
}
