using TaskManagement.Application.Categories.Commands.CreateCategory;
using TaskManagement.Contracts.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainTaskStatus = TaskManagement.Domain.Tasks.TaskStatus;
using TaskManagement.Application.Categories.Queries.GetCategory;
using TaskManagement.Application.Categories.Commands.DeleteCategory;
using TaskManagement.Application.Categories.Queries.ListCategories;

namespace TaskManagement.Api.Controllers;

[Route("categories")]
public class CategoriesController : ApiController
{
    private readonly ISender _mediator;

    public CategoriesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(
        CreateCategoryRequest request)
    {
        var command = new CreateCategoryCommand(request.Name);

        var createCategoryResult = await _mediator.Send(command);
        return createCategoryResult.MatchFirst(
            category => Ok(new CategoryResponse(category.Id, category.Name)),
            error => Problem(error));
    }

    [HttpGet("{categoryId:guid}")]
    public async Task<IActionResult> GetCategory(Guid categoryId)
    {
        var query = new GetCategoryQuery(categoryId);

        var getCategoryResult = await _mediator.Send(query);

        return getCategoryResult.Match(
            category => Ok(new CategoryResponse(
                category.Id,
                category.Name)),
            Problem);
    }

    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategory(Guid categoryId)
    {
        var command = new DeleteCategoryCommand(categoryId);

        var deleteCategoryResult = await _mediator.Send(command);

        return deleteCategoryResult.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListCategories()
    {
        var command = new ListCategoriesQuery();

        var listCategoriesResult = await _mediator.Send(command);

        return listCategoriesResult.Match(
            categories => Ok(categories.ConvertAll(category => new CategoryResponse(category.Id, category.Name))),
            Problem);
    }
}