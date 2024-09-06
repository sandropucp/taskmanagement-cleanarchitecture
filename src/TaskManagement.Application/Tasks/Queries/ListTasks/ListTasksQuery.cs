using ErrorOr;
using MediatR;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Tasks.Queries.ListTasks;

public record ListTasksQuery : IRequest<ErrorOr<List<Local.Task>>>;
