using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.WorkItems;
using TaskManagement.Infrastructure.Common.Persistence;

namespace TaskManagement.Infrastructure.WorkItems.Persistence;

public class WorkItemsRepository(TaskManagementDbContext dbContext) : IWorkItemsRepository
{
    private readonly TaskManagementDbContext dbContext = dbContext;

    public async Task AddWorkItemAsync(WorkItem workItem) => await dbContext.WorkItems.AddAsync(workItem);

    public Task<bool> ExistsAsync(Guid id) => throw new NotImplementedException();

    public async Task<List<WorkItem>> GetAllAsync() => await dbContext.WorkItems.ToListAsync();

    public async Task<WorkItem?> GetByIdAsync(Guid workItemId) => await dbContext.WorkItems.FirstOrDefaultAsync(workItem => workItem.Id == workItemId);

    public Task UpdateWorkItemAsync(WorkItem workItem)
    {
        dbContext.WorkItems.Update(workItem);
        return Task.CompletedTask;
    }

    Task IWorkItemsRepository.RemoveWorkItemAsync(WorkItem workItem)
    {
        dbContext.WorkItems.Remove(workItem);
        return Task.CompletedTask;
    }

    public async Task<List<WorkItem>> GetWorkItemsByCategoryIdAsync(Guid categoryId) => await dbContext.WorkItems.Where(workItem => workItem.CategoryId == categoryId).ToListAsync();
    public Task RemoveRangeAsync(List<WorkItem> workItems)
    {
        dbContext.RemoveRange(workItems);

        return Task.CompletedTask;
    }
}
