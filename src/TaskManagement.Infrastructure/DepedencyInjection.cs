using TaskManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Infrastructure.Common.Persistence;
using TaskManagement.Infrastructure.Tasks.Persistence;
using TaskManagement.Infrastructure.Users.Persistence;
using TaskManagement.Infrastructure.Comments.Persistence;
using TaskManagement.Infrastructure.Attachments.Persistence;

namespace TaskManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddPersistence();
    }
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<TaskManagementDbContext>(options =>
            options.UseSqlite("Data Source = TaskManagement.db"));

        services.AddScoped<ITasksRepository, TasksRepository>();
        services.AddScoped<ICommentsRepository, CommentsRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IAttachmentsRepository, AttachmentsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<TaskManagementDbContext>());

        return services;
    }
}