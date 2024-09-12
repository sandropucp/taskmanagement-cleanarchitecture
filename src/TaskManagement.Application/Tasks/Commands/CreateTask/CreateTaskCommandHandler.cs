using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Tasks;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Tasks.Commands.CreateTask;

public class CreateTaskCommandHandler(ITasksRepository tasksRepository,
    ICategoriesRepository categoriesRepository,
    IUnitOfWork unitOfWork) :
    IRequestHandler<CreateTaskCommand, ErrorOr<Local.Task>>
{
    private readonly ITasksRepository tasksRepository = tasksRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<ErrorOr<Local.Task>> Handle(
        CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetByIdAsync(request.CategoryId);

        if (category is null)
        {
            return TaskErrors.CategoryNotFound;
        }
        var task = new Local.Task(
            name: request.Name, description: request.Description,
            dueDate: request.DueDate, taskStatus: request.TaskStatus,
            categoryId: request.CategoryId,
            categoryName: category.Name,
            assignedToId: request.UserId);

        await tasksRepository.AddTaskAsync(task);
        await unitOfWork.CommitChangesAsync();

        return task;
    }
}
