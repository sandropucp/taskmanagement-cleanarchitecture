using ErrorOr;
using MediatR;
using Local = TaskManagement.Domain.Tasks;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Tasks.Queries.ListTasks;

public class ListTasksQueryHandler : IRequestHandler<ListTasksQuery, ErrorOr<List<Local.Task>>>
{
    private readonly ITasksRepository _tasksRepository;

    public ListTasksQueryHandler(ITasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    public async Task<ErrorOr<List<Local.Task>>> Handle(ListTasksQuery query,
        CancellationToken cancellationToken)
    {
        var tasks = await _tasksRepository.GetAllAsync();
        return tasks;
    }
}