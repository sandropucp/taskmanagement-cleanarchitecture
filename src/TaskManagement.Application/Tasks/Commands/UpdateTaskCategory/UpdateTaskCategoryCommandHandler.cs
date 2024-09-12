using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Tasks.Commands.UpdateTaskCategory;

public class UpdateTaskCategoryCommandHandler(ITasksRepository tasksRepository,
    ICategoriesRepository categoriesRepository,
    IUnitOfWork unitOfWork) :
    IRequestHandler<UpdateTaskCategoryCommand, ErrorOr<Local.Task>>
{
    public async Task<ErrorOr<Local.Task>> Handle(
        UpdateTaskCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetByIdAsync(request.CategoryId);
        var task = await tasksRepository.GetByIdAsync(request.TaskId);
        if (category is null)
        {
            return Error.NotFound("description: category not found");
        }
        if (task is null)
        {
            return Error.NotFound("description: task not found");
        }
        task.UpdateCategory(category);
        await tasksRepository.UpdateTaskAsync(task);
        await unitOfWork.CommitChangesAsync();

        return task;
    }
}
