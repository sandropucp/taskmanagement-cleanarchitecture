namespace TestCommon.TestConstants;

public static partial class Constants
{
    public static class Comment
    {
        public static readonly Guid Id = Guid.NewGuid();
        public const string CommentText = "Comment text";
        public static readonly Guid WorkItemId = Guid.NewGuid();
    }
}