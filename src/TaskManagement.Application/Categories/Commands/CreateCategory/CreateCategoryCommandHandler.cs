using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Categories;

namespace TaskManagement.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(
    ICategoriesRepository categoriesRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, ErrorOr<Category>>
{
    public async Task<ErrorOr<Category>> Handle(CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = new Category(request.Name);
        await categoriesRepository.AddCategoryAsync(category);
        await unitOfWork.CommitChangesAsync();
        return category;
    }
}
