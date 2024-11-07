using TaskManagement.Api;
using TaskManagement.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TaskManagement.Application.SubcutaneousTests.Common;

public class MediatorFactory : WebApplicationFactory<IAssemblyMarker>, IAsyncLifetime
{
    private SqliteTestDatabase _testDatabase = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _testDatabase = SqliteTestDatabase.CreateAndInitialize();

        builder.ConfigureTestServices(services =>
        {
            services
                .RemoveAll<DbContextOptions<TaskManagementDbContext>>()
                .AddDbContext<TaskManagementDbContext>((sp, options) => options.UseSqlite(_testDatabase.Connection));
        });
    }

    public IMediator CreateMediator()
    {
        var serviceScope = Services.CreateScope();

        _testDatabase.ResetDatabase();

        return serviceScope.ServiceProvider.GetRequiredService<IMediator>();
    }


    public Task InitializeAsync() => Task.CompletedTask;

    public new Task DisposeAsync()
    {
        _testDatabase.Dispose();

        return Task.CompletedTask;
    }

    // protected override void ConfigureWebHost(IWebHostBuilder builder)
    // {
    //     var connectionString = "Server=localhost;Database=TaskManagement;User Id=sa;Password=Password123!;";

    //     _testDatabase = SqliteTestDatabase.CreateAndInitialize();

    //     builder.ConfigureTestServices(services =>
    //     {
    //         services
    //             .RemoveAll<DbContextOptions<TaskManagementDbContext>>()
    //             .AddDbContext<TaskManagementDbContext>((sp, options) => options.UseSqlServer(connectionString));
    //     });
    // }

    // public IMediator CreateMediator()
    // {
    //     var serviceScope = Services.CreateScope();

    //     _testDatabase.ResetDatabase();

    //     return serviceScope.ServiceProvider.GetRequiredService<IMediator>();
    // }


    // public Task InitializeAsync() => Task.CompletedTask;

    // public new Task DisposeAsync()
    // {
    //     _testDatabase.Dispose();

    //     return Task.CompletedTask;
    // }
}