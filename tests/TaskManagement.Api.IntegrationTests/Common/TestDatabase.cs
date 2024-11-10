using System.Data.Common;
using Microsoft.Data.SqlClient;
//using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Infrastructure.Common.Persistence;

namespace TaskManagement.Api.IntegrationTests.Common;

/// <summary>
/// We're using SQLite so no need to spin an actual database.
/// </summary>
public class SqlServerTestDatabase : IDisposable
{
    public DbConnection Connection { get; }

    public static SqlServerTestDatabase CreateAndInitialize()
    {
        var testDatabase = new SqlServerTestDatabase("Server=(localdb)\\MSSQLLocalDB;Database=DeploymentProjectDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        testDatabase.InitializeDatabase();

        return testDatabase;
    }

    public void InitializeDatabase()
    {
        Connection.Open();
        var options = new DbContextOptionsBuilder<TaskManagementDbContext>()
            .UseSqlServer(Connection)
            .Options;

        var context = new TaskManagementDbContext(options, null!, null!);

        context.Database.EnsureCreated();
    }

    public void ResetDatabase()
    {
        Connection.Close();
        InitializeDatabase();
    }

    private SqlServerTestDatabase(string connectionString) =>
        Connection = new SqlConnection(connectionString);
    public void Dispose() => GC.SuppressFinalize(this);
}
