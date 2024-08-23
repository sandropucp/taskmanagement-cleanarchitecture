using ErrorOr;
using MediatR;
using TaskManagement.Domain.Categories;

namespace TaskManagement.Application.Categories.Queries.GetCategory;

public record GetCategoryQuery(Guid CategoryId) : IRequest<ErrorOr<Category>>;