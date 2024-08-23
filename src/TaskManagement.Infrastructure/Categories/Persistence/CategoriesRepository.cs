using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Categories;
using TaskManagement.Infrastructure.Common.Persistence;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly TaskManagementDbContext _dbContext;

    public CategoriesRepository(TaskManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddCategoryAsync(Category category)
    {
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid categoryId)
    {
        return await _dbContext.Categories.FirstOrDefaultAsync(category => category.Id == categoryId);
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public Task RemoveCategoryAsync(Category category)
    {
        _dbContext.Remove(category);
        return Task.CompletedTask;
    }
}