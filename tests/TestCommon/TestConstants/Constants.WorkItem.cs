using TaskManagement.Domain.WorkItems;

namespace TestCommon.TestConstants;

public static partial class Constants
{
    public static class WorkItems
    {
        public static readonly Guid Id = Guid.NewGuid();
        public static readonly string DefaultName = "DefaultName";
        public static readonly string DefaultDescription = "DefaultDescription";
        public static readonly DateTime DefaultDueDate = DateTime.Now.AddDays(1);
        public static readonly WorkItemStatus DefaultTaskStatus = WorkItemStatus.InProgress;
        public static readonly Guid DefaultCategoryId = Guid.NewGuid();
        public static readonly Guid DefaultUserAssignedToId = Guid.NewGuid();
    }
    // {
    //     public static readonly WorkItemStatus DefaultTaskStatus = WorkItemStatus.InProgress;
    //     public static readonly Guid Id = Guid.NewGuid();
    //     public const int MaxAttachments = 3;
    //     public const int MaxComments = 1;
    // }
}
