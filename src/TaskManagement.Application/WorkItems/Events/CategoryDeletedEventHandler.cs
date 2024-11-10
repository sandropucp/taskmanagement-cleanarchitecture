using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Users.Events;

namespace TaskManagement.Application.Tasks.Events;

public class CategoryDeletedEventHandler(
    IWorkItemsRepository workItemsRepository,
    IUnitOfWork unitOfWork) : INotificationHandler<CategoryDeletedEvent>
{
    public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        var workItems = await workItemsRepository.GetWorkItemsByCategoryIdAsync(notification.CategoryId);

        await workItemsRepository.RemoveRangeAsync(workItems);
        await unitOfWork.CommitChangesAsync();
    }
}
