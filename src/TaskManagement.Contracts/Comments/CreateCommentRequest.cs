namespace TaskManagement.Contracts.Comments;

public record CreateCommentRequest(Guid TaskId, Guid UserId, string CommentText);