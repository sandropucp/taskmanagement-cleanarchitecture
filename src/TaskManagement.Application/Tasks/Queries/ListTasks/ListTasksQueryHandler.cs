using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Tasks.Queries.ListTasks;

public class ListTasksQueryHandler(ITasksRepository tasksRepository) : IRequestHandler<ListTasksQuery, ErrorOr<List<Local.Task>>>
{
    private readonly ITasksRepository tasksRepository = tasksRepository;

    public async Task<ErrorOr<List<Local.Task>>> Handle(ListTasksQuery request,
        CancellationToken cancellationToken)
    {
        var tasks = await tasksRepository.GetAllAsync();
        return tasks;
    }
}
