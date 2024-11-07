using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Application.WorkItems.Queries.ListWorkItems;
using TaskManagement.Domain.WorkItems;

namespace TaskManagement.Application.Tasks.Queries.ListWorkItems;

public class ListWorkItemsQueryHandler(IWorkItemsRepository workItemsRepository) : IRequestHandler<ListWorkItemsQuery, ErrorOr<List<WorkItem>>>
{
    private readonly IWorkItemsRepository workItemsRepository = workItemsRepository;

    public async Task<ErrorOr<List<WorkItem>>> Handle(ListWorkItemsQuery request,
        CancellationToken cancellationToken)
    {
        var workItems = await workItemsRepository.GetAllAsync();
        return workItems;
    }
}
