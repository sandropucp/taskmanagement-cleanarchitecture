using TaskManagement.Domain.WorkItems;

namespace TaskManagement.Application.Common.Interfaces;

public interface IWorkItemsRepository
{
    Task AddWorkItemAsync(WorkItem workItem);
    Task<WorkItem?> GetByIdAsync(Guid workItemId);
    Task<List<WorkItem>> GetAllAsync();
    Task UpdateWorkItemAsync(WorkItem workItem);
    Task<bool> ExistsAsync(Guid id);
    Task RemoveWorkItemAsync(WorkItem workItem);
    Task<List<WorkItem>> GetWorkItemsByCategoryIdAsync(Guid categoryId);
    Task RemoveRangeAsync(List<WorkItem> workItems);
}
