using TaskManagement.Domain.Categories;

namespace TaskManagement.Application.Common.Interfaces;

public interface ICategoriesRepository
{
    Task AddCategoryAsync(Category category);
    Task<Category?> GetByIdAsync(Guid categoryId);
    Task<List<Category>> GetAllAsync();
    Task RemoveCategoryAsync(Category category);
}