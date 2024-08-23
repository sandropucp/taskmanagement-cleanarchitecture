using ErrorOr;
using TaskManagement.Application.Common.Interfaces;
using MediatR;

namespace TaskManagement.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ErrorOr<Deleted>>
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(
        IUnitOfWork unitOfWork,
        ICategoriesRepository categoriesRepository)
    {
        _unitOfWork = unitOfWork;
        _categoriesRepository = categoriesRepository;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetByIdAsync(command.CategoryId);

        if (category is null)
        {
            return Error.NotFound(description: "category not 1 found");
        }

        await _categoriesRepository.RemoveCategoryAsync(category);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}
