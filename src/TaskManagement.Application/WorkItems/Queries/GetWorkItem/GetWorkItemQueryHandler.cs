using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.WorkItems;

namespace TaskManagement.Application.WorkItems.Queries.GetWorkItem;

public class GetTaskQueryHandler(IWorkItemsRepository workItemsRepository) : IRequestHandler<GetWorkItemQuery, ErrorOr<WorkItem>>
{
    private readonly IWorkItemsRepository workItemsRepository = workItemsRepository;

    public async Task<ErrorOr<WorkItem>> Handle(GetWorkItemQuery request,
        CancellationToken cancellationToken)
    {
        var workItem = await workItemsRepository.GetByIdAsync(request.WorkItemId);
        return workItem is null
            ? Error.NotFound(description: "Task not found")
            : workItem;
    }
}
