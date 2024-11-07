using TaskManagement.Application.Comments.Commands.CreateComment;
using TestCommon.TestConstants;

namespace TestCommon.Comments;

public static class CommentCommandFactory
{
    public static CreateCommentCommand CreateCreateCommentCommand(
        Guid? workItemId = null,
        Guid? userId = null,
        string commentText = Constants.Comment.CommentText) => new(
            WorkItemId: workItemId ?? Constants.Comment.WorkItemId,
            UserId: userId ?? Constants.User.Id,
            CommentText: commentText);
}