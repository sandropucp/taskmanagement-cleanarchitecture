using ErrorOr;
using MediatR;
using TaskManagement.Domain.Categories;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Categories.Queries.ListCategories;

public class ListCategoriesQueryHandler : IRequestHandler<ListCategoriesQuery, ErrorOr<List<Category>>>
{
    private readonly ICategoriesRepository _categoriesRepository;

    public ListCategoriesQueryHandler(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<ErrorOr<List<Category>>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoriesRepository.GetAllAsync();
        return categories.ToList();
    }
}