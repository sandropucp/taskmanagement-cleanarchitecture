using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Infrastructure.Common.Persistence;

public class TaskManagementDbContext : DbContext, IUnitOfWork
{
    public DbSet<Local.Task> Tasks { get; set; } = null!;

    public TaskManagementDbContext(DbContextOptions options) : base(options){}

    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}