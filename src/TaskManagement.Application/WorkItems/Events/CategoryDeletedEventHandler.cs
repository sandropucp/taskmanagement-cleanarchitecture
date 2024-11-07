using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Users.Events;
using MediatR;

namespace TaskManagement.Application.Tasks.Events;

public class CategoryDeletedEventHandler(
    IWorkItemsRepository workItemsRepository,
    IUnitOfWork unitOfWork): INotificationHandler<CategoryDeletedEvent>
{
    private readonly IWorkItemsRepository workItemsRepository = workItemsRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        var workItems = await workItemsRepository.GetWorkItemsByCategoryIdAsync(notification.CategoryId);

        await workItemsRepository.RemoveRangeAsync(workItems);
        await unitOfWork.CommitChangesAsync();
    }
}
