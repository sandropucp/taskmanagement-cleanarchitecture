using ErrorOr;
using MediatR;

namespace TaskManagement.Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid CategoryId) : IRequest<ErrorOr<Deleted>>;
