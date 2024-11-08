using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;


namespace TaskManagement.Application.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork,
    ICategoriesRepository categoriesRepository) : IRequestHandler<DeleteCategoryCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetByIdAsync(request.CategoryId);

        if (category is null)
        {
            return Error.NotFound(description: "category not 1 found");
        }

        var user = await usersRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            return Error.Unexpected(description: "Admin not found");
        }

        user.DeleteCategory(request.CategoryId);

        // Do this asynchronusly with Event Sourcing
        //await categoriesRepository.RemoveCategoryAsync(category);
        //await usersRepository.UpdateAsync(admin);
        await unitOfWork.CommitChangesAsync(); // FYI: This will publish the domain events

        return Result.Deleted;
    }
}
