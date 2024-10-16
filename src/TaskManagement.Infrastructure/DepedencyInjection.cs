using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Infrastructure.Admins.Persistence;
using TaskManagement.Infrastructure.Attachments.Persistence;
using TaskManagement.Infrastructure.Categories.Persistence;
using TaskManagement.Infrastructure.Comments.Persistence;
using TaskManagement.Infrastructure.Common.Persistence;
using TaskManagement.Infrastructure.Tasks.Persistence;
using TaskManagement.Infrastructure.Users.Persistence;

namespace TaskManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) => services.AddPersistence(configuration);

    // public static IServiceCollection AddInfrastructure(this IServiceCollection services) => services
    //         .AddPersistence();
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ProjectContext");
        services.AddDbContext<TaskManagementDbContext>(options =>
            options.UseSqlServer(connectionString));
        // services.AddDbContext<TaskManagementDbContext>(options =>
        //     options.AddInterceptors(new AuditInterceptor()));
        services.AddKeyedScoped<List<AuditEntry>>("Audit", (_, _) => new());

        services.AddScoped<ITasksRepository, TasksRepository>();
        services.AddScoped<ICommentsRepository, CommentsRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IAttachmentsRepository, AttachmentsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IAdminsRepository, AdminsRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<TaskManagementDbContext>());

        return services;
    }
}
