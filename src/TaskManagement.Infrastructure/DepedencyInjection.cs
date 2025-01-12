using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Api.Authentication.PasswordHasher;
using TaskManagement.Api.Authentication.TokenGenerator;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.AuditEntries;
using TaskManagement.Domain.Common.Interfaces;
using TaskManagement.Infrastructure.Attachments.Persistence;
using TaskManagement.Infrastructure.Categories.Persistence;
using TaskManagement.Infrastructure.Comments.Persistence;
using TaskManagement.Infrastructure.Common.Persistence;
using TaskManagement.Infrastructure.Users.Persistence;
using TaskManagement.Infrastructure.WorkItems.Persistence;


namespace TaskManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string cs) =>
        services
            .AddAuthentication(configuration)
            .AddPersistence(configuration, cs);

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration, string connectionString)
    {
        //var password = @"Password1*";
        //var connectionString = configuration.GetConnectionString("DefaultConnection");
        //connectionString = string.Format(connectionString, password);
        Console.WriteLine(connectionString);

        //var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<TaskManagementDbContext>(options =>
            options.UseSqlServer(connectionString));
        // services.AddDbContext<TaskManagementDbContext>(options =>
        //     options.AddInterceptors(new AuditInterceptor()));
        services.AddKeyedScoped<List<AuditEntry>>("Audit", (_, _) => new());

        services.AddScoped<IWorkItemsRepository, WorkItemsRepository>();
        services.AddScoped<ICommentsRepository, CommentsRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<IAttachmentsRepository, AttachmentsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<TaskManagementDbContext>());

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.Section, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            });


        return services;
    }
}
