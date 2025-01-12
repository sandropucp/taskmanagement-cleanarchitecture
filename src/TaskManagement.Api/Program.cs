using TaskManagement.Api;
using TaskManagement.Application;
using TaskManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var password = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");
    connectionString = string.Format(connectionString, password);

    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration, connectionString);
}

var app = builder.Build();
{
    app.UseExceptionHandler();
    app.AddInfrastructureMiddleware();

    // if (app.Environment.IsDevelopment())
    // {
    //     app.UseSwagger();
    //     app.UseSwaggerUI();
    // }

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
