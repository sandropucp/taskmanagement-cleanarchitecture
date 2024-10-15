using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;


namespace TaskManagement.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(
    IAdminsRepository adminsRepository,
    IUnitOfWork unitOfWork,
    ICategoriesRepository categoriesRepository) : IRequestHandler<DeleteCategoryCommand, ErrorOr<Deleted>>
{
    private readonly IAdminsRepository adminsRepository = adminsRepository;
    private readonly ICategoriesRepository categoriesRepository = categoriesRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task<ErrorOr<Deleted>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetByIdAsync(request.CategoryId);

        if (category is null)
        {
            return Error.NotFound(description: "category not 1 found");
        }

        var admin = await adminsRepository.GetByIdAsync(category.AdminId);

        if (admin is null)
        {
            return Error.Unexpected(description: "Admin not found");
        }

        admin.DeleteCategory(request.CategoryId);

        //await categoriesRepository.RemoveCategoryAsync(category);
        await adminsRepository.UpdateAsync(admin);
        await unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}
