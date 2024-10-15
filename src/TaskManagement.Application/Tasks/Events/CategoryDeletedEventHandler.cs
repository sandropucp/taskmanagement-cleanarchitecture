using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Admins.Events;
using MediatR;

namespace TaskManagement.Application.Tasks.Events;

public class CategoryDeletedEventHandler(
    ITasksRepository tasksRepository,
    IUnitOfWork unitOfWork): INotificationHandler<CategoryDeletedEvent>
{
    private readonly ITasksRepository tasksRepository = tasksRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        var tasks = await tasksRepository.GetTasksByCategoryIdAsync(notification.CategoryId);

        await tasksRepository.RemoveRangeAsync(tasks);
        await unitOfWork.CommitChangesAsync();
    }
}
