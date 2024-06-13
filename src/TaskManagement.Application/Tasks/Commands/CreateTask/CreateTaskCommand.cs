using ErrorOr;
using MediatR;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Tasks.Commands.CreateTask;

public record CreateTaskCommand(string Name) : 
    IRequest<ErrorOr<Local.Task>>;