using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Categories;
using TaskManagement.Infrastructure.Common.Persistence;

namespace TaskManagement.Infrastructure.Categories.Persistence;
public class CategoriesRepository(TaskManagementDbContext dbContext) : ICategoriesRepository
{
    private readonly TaskManagementDbContext dbContext = dbContext;

    public async Task AddCategoryAsync(Category category)
    {
        await dbContext.Categories.AddAsync(category);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid categoryId) => await dbContext.Categories.FirstOrDefaultAsync(category => category.Id == categoryId);

    public async Task<List<Category>> GetAllAsync() => await dbContext.Categories.ToListAsync();

    public Task RemoveCategoryAsync(Category category)
    {
        dbContext.Remove(category);
        return Task.CompletedTask;
    }
}
