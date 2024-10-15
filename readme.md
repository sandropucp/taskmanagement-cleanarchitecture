# Clean Architecture

## Introduction to Clean Architecture

Clean Architecture is a software design philosophy introduced by Robert C. Martin, also known as Uncle Bob. It emphasizes the separation of concerns and the organization of code in a way that makes it more maintainable, testable, and scalable. The key principles of Clean Architecture include:

### Key Principles

1. **Independence of Frameworks**: The architecture should not be dependent on any external frameworks. Frameworks should be treated as tools rather than part of the core business logic.

2. **Testability**: The design should facilitate easy testing. By isolating the core logic from external dependencies, unit testing becomes straightforward and reliable.

3. **UI Independence**: The user interface should be decoupled from the business logic. This allows for changes in the UI without affecting the underlying system.

4. **Database Independence**: The business logic should not depend on the database. This allows for flexibility in changing or updating the database technology without impacting the core logic.

5. **Separation of Concerns**: The architecture should clearly separate different areas of concern, such as business rules, UI, and data access. This modular approach helps in managing complexity and improving maintainability.

### Structure of Clean Architecture

The structure of Clean Architecture is typically depicted as a series of concentric circles, each representing different layers of the system:

- **Entities (Domain)**: Represent the core business logic and enterprise-wide rules. They are the most inner circle and are independent of any external system.
- **Use Cases (Application)**: Encapsulate the application's business rules and orchestrate the flow of data to and from the entities.
- **Interface Adapters (Presentation)**: Convert data from the use cases to a format suitable for frameworks and external systems such as databases, web, or external APIs.
- **Frameworks and Drivers (Infrastructure)**: The outermost layer includes UI frameworks, databases, and external tools, which interact with the interface adapters.

This layered approach ensures that each part of the system is only aware of the adjacent layers, promoting low coupling and high cohesion. The dependencies point inward, ensuring that high-level policies are not dependent on low-level details.

## Setup

### Create Projects using CLI
```
cd src

dotnet new webapi -controllers -n TaskManagement.Api  
dotnet new classlib -o TaskManagement.Contracts
dotnet new classlib -o TaskManagement.Infrastructure
dotnet new classlib -o TaskManagement.Application
dotnet new classlib -o TaskManagement.Domain

dotnet add TaskManagement.Api reference TaskManagement.Application
dotnet add TaskManagement.Api reference TaskManagement.Contracts
dotnet add TaskManagement.Api reference TaskManagement.Infrastructure
dotnet add TaskManagement.Infrastructure reference TaskManagement.Application
dotnet add TaskManagement.Application reference TaskManagement.Domain

cd..

dotnet new sln --name "TaskManagement"
dotnet sln add (ls -r **/**.csproj)  
dotnet build
```

### Install external libraries
```
cd src

dotnet add TaskManagement.Domain package ErrorOr
dotnet add TaskManagement.Domain package Throw
dotnet add TaskManagement.Domain package Ardalis.SmartEnum
dotnet add TaskManagement.Application package ErrorOr
dotnet add TaskManagement.Application package MediatR
dotnet add TaskManagement.Application package FluentValidation
dotnet add TaskManagement.Application package FluentValidation.AspNetCore
dotnet add TaskManagement.Application package Microsoft.Extensions.DependencyInjection.Abstractions
dotnet add TaskManagement.Infrastructure package Microsoft.EntityFrameworkCore
dotnet add TaskManagement.Infrastructure package Microsoft.EntityFrameworkCore.Sqlite
dotnet add TaskManagement.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
dotnet add TaskManagement.Api package Microsoft.AspNetCore.OpenApi
dotnet add TaskManagement.Api package Microsoft.EntityFrameworkCore.Design
```

### Install Entity Framework (DO IT IN) (https://www.entityframeworktutorial.net/efcore/cli-commands-for-ef-core-migration.aspx)
- Install entity framework in computer
```
dotnet tool install --global dotnet-ef --version 8.*
```

- Create folder migrations in the **Infrastructure** project
```
dotnet ef migrations list InitialCreate -p src/TaskManagement.Infrastructure -s src/TaskManagement.Api
```

- Create **Database** in the **Api** project
```
dotnet ef database update -p src/TaskManagement.Infrastructure -s src/TaskManagement.Api
```

- After **Update Database**

```
dotnet ef migrations add XXXNAMEXXX -p src/TaskManagement.Infrastructure -s src/TaskManagement.Api
dotnet ef database update -p src/TaskManagement.Infrastructure -s src/TaskManagement.Api

Ex:
dotnet ef migrations add EventsV1 -p src/TaskManagement.Infrastructure -s src/TaskManagement.Api
dotnet ef database update -p src/TaskManagement.Infrastructure -s src/TaskManagement.Api
```

### Run Client API
```
dotnet run --project src/TaskManagement.Api
```


## Domain

The main objectives of this layer is to define the Domain Models, Domain Errors, Execute Business Logic and enforcing Business Rules. We will use **Rich Domain Models** instead of **Anemic Domain Models**

Create Folder with domain object name. For example: 

- Tasks
    - Task.cs
    - TaskErrors.cs

```
namespace TaskManagement.Domain.Tasks;

public class Task
{
    public Guid Id { get; private set; }
    public string Name { get; init; } = null!;

    private Task(){}
    public Task(string name, Guid? id = null)
    {
        Name = name;
        Id = id ?? Guid.NewGuid();
    }
}
```

```
using ErrorOr;

namespace TaskManagement.Domain.Tasks;

public static class TaskErrors
{
    public static readonly Error CannotNotHaveName = Error.Validation(
        code: "Task.CannotNotHaveName",
        description: "A task cannot not have name");
}
```
As we can see in the code we applied the Result Pattern to handle the exceptions

## Application Layer (Use Cases)

- This layer is responsile to execute the application Use Cases. In other words is all the actions that the user can do in the system.
- Fetch domain objects.
- Manipulate domain objects.

Examples of Use Cases:

    - Create Task
    - List Tasks
    - Complete Task
    - Update Task
    - Delete Task
    - Create Category

- For each domain object create a folder with two subfolders Queries and Commands. For example 
    - Tasks Folder
        - Commands.
            - CreateTask
                - CreateTaskCommand.cs
                - CreateTaskCommandHandler.cs
            - DeleteTask
        - Queries
            - GetTask
                - ListTaskCommand.cs
                - ListTaskCommandHandler.cs


```
using ErrorOr;
using MediatR;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Tasks.Commands.CreateTask;

public record CreateTaskCommand(string Name) : 
    IRequest<ErrorOr<Local.Task>>;
```

```
using ErrorOr;
using MediatR;
using Local = TaskManagement.Domain.Tasks;
using TaskManagement.Application.Common.Interfaces;

namespace TaskManagement.Application.Tasks.Commands.CreateTask;

public class CreateTaskCommandHandler : 
    IRequestHandler<CreateTaskCommand, ErrorOr<Local.Task>>
{
    private readonly ITasksRepository _tasksRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTaskCommandHandler(ITasksRepository tasksRepository, IUnitOfWork unitOfWork)
    {
        _tasksRepository = tasksRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Local.Task>> Handle(
        CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new Local.Task(
            name: request.Name);

        await _tasksRepository.AddTaskAsync(task);
        await _unitOfWork.CommitChangesAsync();

        return task;
    }
}
```

### Implementing Repository Pattern
In the **Application Project** create a Common folder

- Common
    - Interfaces
        - ITaskRepository.cs
        - IUnitOfQWork.cs

```
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Application.Common.Interfaces;

public interface ITasksRepository
{
    Task AddTaskAsync(Local.Task task);
}
```

```
namespace TaskManagement.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}
```

## Presentation Layer
We have 2 projects in this layer **Contracts Project** and **API Project**.

### Contracts

- Create Contracts Project independent to be able to publish to Nuget for the Client. We use this project to have a common language between the **Presentation Layer** and the **Application Layer**

- Tasks
    - CreateTaskRequest.cs
    - TaskResponse.cs

- Create class CreateTaskRequest.cs
```
namespace TaskManagement.Contracts.Tasks;

public record CreateTaskRequest(string Name);
```

- Create class TaskReponse.cs
```
namespace TaskManagement.Contracts.Tasks;

public record TaskResponse(Guid Id, string Name);
```

- Define EndPoints

    - Post Task
    - Post User
    - Post Category
    - Post Comment

### API

The API layer will need to use classes from the **Infrastructure Layer** and the **Application Layer** for this we can use **Dependency Injection** and add each class to the Program.cs class or we can create a class DependencyInjection.cs in each Project **(Infrastructure and Application)** and add these 2 classes to Program.cs. In this way the Infrastructure Project and the Application Project are independent of the API.

- Controllers
    - ApiController.cs
    - TasksController.cs

- ApiController.cs
```
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TaskManagement.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        return Problem(errors[0]);
    }

    protected IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, detail: error.Description);
    }

    protected IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }
}
```

- TasksController.cs
```
using ErrorOr;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Contracts.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Api.Controllers;

[Route("tasks")]
public class TasksController : ApiController
{
    private readonly ISender _mediator;

    public TasksController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(
        CreateTaskRequest request)
    {
        var command = new CreateTaskCommand(
            request.Name);

        var createTaskResult = await _mediator.Send(command);
        return createTaskResult.MatchFirst(
            task => Ok(new TaskResponse(task.Id, task.Name)),
            error => Problem(error));
    }
}
```

- Create Root Request Folder to Test Api
- Create REST request
- CreateTask.http
```
@HostAddress = http://localhost:5147

POST {{HostAddress}}/tasks/
Content-Type: application/json

{
  "Name": "Task 1"
}
```

## Infrastructure Layer

- Interacting with the persistence solution
- Interacting with other services (web clients, message brokers, etc)
- Interacting with the underlying machine (system clock, files, etc)
- Identity concerns

- Create a Common --> Persistnce Folder

    - Common
        - Persistence
            - FluentApiExtension.cs
            - TaskManagementDbContext.cs

- TaskManagementDbContext
```
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
```

- Create Tasks --> Persistence Folder
    - Tasks
        - Persistence
            - TaskRepository.cs

- TaskRepository.cs
```
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Infrastructure.Common.Persistence;
using Local = TaskManagement.Domain.Tasks;

namespace TaskManagement.Infrastructure.Tasks.Persistence;

public class TasksRepository : ITasksRepository
{
    private readonly TaskManagementDbContext _dbContext;

    public TasksRepository(TaskManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddTaskAsync(Local.Task task)
    {
        await _dbContext.Tasks.AddAsync(task);
    }
}
```

- Create a file to handle Dependency Injection for the API (DependencyInjection.cs)
```
using TaskManagement.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Infrastructure.Common.Persistence;
using TaskManagement.Infrastructure.Tasks.Persistence;

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
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<TaskManagementDbContext>());

        return services;
    }
}
```

