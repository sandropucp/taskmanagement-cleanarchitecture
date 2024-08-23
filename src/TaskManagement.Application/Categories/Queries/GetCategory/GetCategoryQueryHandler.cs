using ErrorOr;
using MediatR;
using TaskManagement.Domain.Categories;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Categories.Queries.GetCategory;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ErrorOr<Category>>
{
    private readonly ICategoriesRepository _categoriesRepository;

    public GetCategoryQueryHandler(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<ErrorOr<Category>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetByIdAsync(request.CategoryId);
        return category is null
            ? Error.NotFound(description: "Category not found")
            : category;
    }
}