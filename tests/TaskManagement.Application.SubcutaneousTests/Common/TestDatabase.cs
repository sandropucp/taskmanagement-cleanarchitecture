using TaskManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace TaskManagement.Application.SubcutaneousTests.Common;

/// <summary>
/// In Subcutaneous tests we aren't testing integration with a real database,
/// so even if we weren't using SQLite we would use some in-memory database.
/// </summary>
public class SqliteTestDatabase : IDisposable
{
    public SqliteConnection Connection { get; }

    public static SqliteTestDatabase CreateAndInitialize()
    {
        var testDatabase = new SqliteTestDatabase("DataSource=:memory:");

        testDatabase.InitializeDatabase();

        return testDatabase;
    }

    public void InitializeDatabase()
    {
        Connection.Open();
        var options = new DbContextOptionsBuilder<TaskManagementDbContext>()
            .UseSqlite(Connection)
            .Options;

        var context = new TaskManagementDbContext(options, null!, null!);

        context.Database.EnsureCreated();
    }

    public void ResetDatabase()
    {
        Connection.Close();

        InitializeDatabase();
    }

    private SqliteTestDatabase(string connectionString)
    {
        Connection = new SqliteConnection(connectionString);
    }

    public void Dispose()
    {
        Connection.Close();
    }
}



// public class SqlServerTestDatabase : IDisposable
//{
    //public SqlServerConnection Connection { get; }

    // public static SqlServerTestDatabase CreateAndInitialize()
    // {
    //     var testDatabase = new SqlServerTestDatabase("DataSource=:memory:");

    //     testDatabase.InitializeDatabase();

    //     return testDatabase;
    // }

    // public void InitializeDatabase()
    // {
    //     //Connection.Open();
    //     var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=DeploymentProjectDb;Trusted_Connection=True;MultipleActiveResultSets=true";

    //     var options = new DbContextOptionsBuilder<TaskManagementDbContext>()
    //         .UseSqlServer(connectionString)
    //         .Options;

    //     var context = new TaskManagementDbContext(options, null!, null!);

    //     context.Database.EnsureCreated();
    // }

//     public void ResetDatabase()
//     {
//         //Connection.Close();

//         InitializeDatabase();
//     }

//     private SqlServerTestDatabase(string connectionString)
//     {
//         //Connection = new SqlServerConnection(connectionString);
//     }

//     public void Dispose()
//     {
//         //Connection.Close();
//     }
// }