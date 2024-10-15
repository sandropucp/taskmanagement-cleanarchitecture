using ErrorOr;
using MediatR;
using Local = TaskManagement.Domain.Categories;

namespace TaskManagement.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, Guid AdminId) : IRequest<ErrorOr<Local.Category>>;
