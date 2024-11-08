# Clean Architecture (Task Management API)

## Run Application

<details open>
<summary>Content</summary>

## Run Client API

```
dotnet run --project src/TaskManagement.Api
```

## How to Push Updates to GitHub

### Pushing To Master/Main Branch using alias

```
gas             # Git add and status
gc "Comment"    # Git Commit Comment
gpo             # Git Push to Origin
```

### How to create a new Feature and Push updates for PR

```
gnewbr  new_feature           # Checkout master, pull master and create a new branch

Do Updates in project
Using a tool like VS Code check updates (Differential) and reverse what we do not need
After we are 100% sure about out updates

gas                                     # Git add and status
gc "Adding new feature"                 # Git Commit Comment
gpo                                     # Git Push to Origin

git remote set-head origin main/master  # if we see any error here  run

pr                                      # To Open a new Pull Request
```

- Here other option a bit quicker

```
gnewbr  new_feature                     # Checkout master, pull master and create a new branch

Do Updates in project
Using a tool like VS Code check updates (Differential) and reverse what we do not need
After we are 100% sure about out updates

gcpr "Adding new feature"               # Add, Commit, and Open a Pull Request
```

</details>

## Introduction to Clean Architecture 

<details>
<summary><b>Content</b></summary>

Clean Architecture is a software design philosophy introduced by Robert C. Martin, also known as Uncle Bob. It emphasizes the separation of concerns and the organization of code in a way that makes it more maintainable, testable, and scalable. 
The key principles of Clean Architecture include:

### Key Principles

1. **Independence of Frameworks**: The architecture should not be dependent on any external frameworks. Frameworks should be treated as tools rather than part of the core business logic.

2. **Testability**: The design should facilitate easy testing. By isolating the core logic from external dependencies, unit testing becomes straightforward and reliable.

3. **UI Independence**: The user interface should be decoupled from the business logic. This allows for changes in the UI without affecting the underlying system.

4. **Database Independence**: The business logic should not depend on the database. This allows for flexibility in changing or updating the database technology without impacting the core logic.

5. **Separation of Concerns**: The architecture should clearly separate different areas of concern, such as business rules, UI, and data access. This modular approach helps in managing complexity and improving maintainability.

### Structure of Clean Architecture

The structure of Clean Architecture is typically depicted as a series of concentric circles, each representing different layers of the system:

- **Entities (Domain)**: Represent the core business logic and enterprise-wide rules. They are the most inner circle and are independent of any external system.
- **Use Cases (Application)**: Encapsulate the application's business rules and orchestrate the flow of data to and from the entities. Also define the Repositories Interfaces.
- **Interface Adapters (Presentation)**: Convert data from the use cases to a format suitable for frameworks and external systems such as databases, web, or external APIs.
- **Frameworks and Drivers (Infrastructure)**: The outermost layer includes UI frameworks, databases, and external tools, which interact with the interface adapters.

This layered approach ensures that each part of the system is only aware of the adjacent layers, promoting low coupling and high cohesion. The dependencies point inward, ensuring that high-level policies are not dependent on low-level details.

![alt text](/images/Clean-Architecture-Diagram-Asp-Net.jpg)

Final Result:

![alt text](/images/SwaggerV1.jpg)

</details>

## Initial Project Setup

<details>
<summary><b>Content</b></summary>

### Create Projects using CLI

Some commands have to execute in Power Shell
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

### Setup Entity Framework

- Good resource https://www.entityframeworktutorial.net/efcore/cli-commands-for-ef-core-migration.aspx

- Install entity framework in computer

```
# Install globally in your computer
dotnet tool install --global dotnet-ef --version 8.*

# Create folder migrations in the Infrastructure project
dotnet ef migrations list InitialCreate -p src/TaskManagement.Infrastructure -s src/TaskManagement.Api

# Create Initial **Database** in the **Api** project
dotnet ef database update -p src/TaskManagement.Infrastructure -s src/TaskManagement.Api
```


- Push **Updates to Database** after changes in Clases

```
dotnet ef migrations add XXXNAMEXXX -p src/TaskManagement.Infrastructure -s src/TaskManagement.Api
dotnet ef database update -p src/TaskManagement.Infrastructure -s src/TaskManagement.Api

Example:
dotnet ef migrations add EventsV4 -p src/TaskManagement.Infrastructure -s src/TaskManagement.Api
dotnet ef database update -p src/TaskManagement.Infrastructure -s src/TaskManagement.Api
```



### Test

- We will apply 3 types of testing
    - Unit Testing to Domain
    - Subcutaneous Testing to Application
    - Integration Testing to Api

![alt text](/images/TestingV1.jpg)

#### Unit Testing (Domain)

- For this tests we use **Ubiquitous Language**

```
Go to Root
mkdir tests (same src level)

cd tests
dotnet new classlib -o TestCommon 
dotnet new xunit -o TaskManagement.Domain.UnitTests

dotnet add TaskManagement.Domain.UnitTests package FluentAssertions

cd .. (Root)
dotnet add tests/TestCommon reference src/TaskManagement.Domain
dotnet add tests/TaskManagement.Domain.UnitTests reference tests/TestCommon

# This command only run in PowerShell
dotnet sln add (ls -r **/**.csproj) 

```

#### Unit Testing (Application)

- For this tests we use **Ubiquitous Language**

```
cd tests
dotnet new xunit -o TaskManagement.Application.UnitTests

dotnet add TaskManagement.Application.UnitTests package FluentAssertions
dotnet add TaskManagement.Application.UnitTests package NSubstitute

cd .. (Root)
dotnet add tests/TaskManagement.Application.UnitTests reference src/TaskManagement.Application
dotnet add tests/TaskManagement.Application.UnitTests reference tests/TestCommon
dotnet add tests/TestCommon reference src/TaskManagement.Application

dotnet sln add (ls -r **/**.csproj) # This command only run in PowerShell

```


#### Subcutaneous Testing

- For this tests we use **Use Cases**

```
cd tests
dotnet new xunit -o TaskManagement.Application.SubcutaneousTests

cd .. (Root)
dotnet add tests/TaskManagement.Application.SubcutaneousTests reference tests/TestCommon
dotnet add tests/TaskManagement.Application.SubcutaneousTests reference src/TaskManagement.Api

dotnet add tests/TaskManagement.Application.SubcutaneousTests package Microsoft.AspNetCore.Mvc.Testing
dotnet add tests/TaskManagement.Application.SubcutaneousTests package FluentAssertions
dotnet add tests/TaskManagement.Application.SubcutaneousTests package Microsoft.EntityFrameworkCore.Sqlite

dotnet sln add (ls -r **/**.csproj) # This command only run in PowerShell
```

#### Integration Testing

- For this tests we use **Use Cases**

```
cd tests
dotnet new xunit -o TaskManagement.Api.IntegrationTests

cd .. (Root)
dotnet add tests/TaskManagement.Api.IntegrationTests reference tests/TestCommon
dotnet add tests/TaskManagement.Api.IntegrationTests reference src/TaskManagement.Api

dotnet add tests/TaskManagement.Api.IntegrationTests package Microsoft.AspNetCore.Mvc.Testing
dotnet add tests/TaskManagement.Api.IntegrationTests package FluentAssertions

dotnet sln add (ls -r **/**.csproj) # This command only run in PowerShell
```

</details>

## Project Layers

<details>
<summary><b>Content</b></summary>

## 1. Domain

The main objectives of this layer is to define the Domain Models, Domain Errors, Execute Business Logic and enforcing Business Rules. We will use **Rich Domain Models** instead of **Anemic Domain Models**

### Ubiquitous Language

1. A User can creates a new Task.
2. A Admin or Task Owner can assigns a Task to a User who becomes the Assignee.
3. The Task Assignee or Admin can changes the Status of the Task.
4. The Admin or Task Owner can changes the Priority Level of the Task.
5. The User(Assignee) can adds a Comment to the Task.
6. The Admin or Task Owner can deletes a Comment.
7. The Admin or Task Owner can assigns a Category to the Task.
8. The Admin or Task Owner can creates a new Category.
9. The User(Assignee) can filter tasks by Category.
10. The User(Assignee) can views detailed information about a Task.
11. The Admin or Task Owner can modifies the Task.
12. The User(Assignee) or Admin can marks the Task as Completed.


Create Folder with domain object name as plural. For example: 

- UnitWorks
    - UnitWork.cs
    - UnitWorksErrors.cs

```
namespace TaskManagement.Domain.WorkItems;

public class WorkItem : Entity
{
    private readonly int maxAttachments = 10;
    private readonly int maxComments = 10;
    private readonly List<Guid> commentIds = [];
    private readonly List<Guid> attachmentIds = [];

    public string Name { get; init; } = null!;
    public string? Description { get; init; } = null!;
}
```

```
using ErrorOr;

namespace TaskManagement.Domain.WorkItems;

public static class WorkItemErrors
{
    public static readonly Error CannotNotHaveName = Error.Validation(
        code: "WorkItem.CannotNotHaveName",
        description: "A task cannot not have name");

    public static readonly Error CannotNotHaveStatus = Error.Validation(
        code: "WorkItem.CannotNotHaveStatus",
        description: "A task cannot not have status");
}
```

## 2. Application Layer (Use Cases)

- This layer is responsile to execute the application Use Cases. In other words is all the actions that the user can do in the system.
- Fetch domain objects.
- Manipulate domain objects.
- We applied the Result Pattern to handle the exceptions
- We applied validation using Fluent Validation


### Use Cases

1. A User creates a new Task by providing a Description, Due Date, Priority Level, and optionally assigning a Category.
2. The Admin or Task Owner assigns a Task to a User who becomes the Assignee responsible for completing the Task.
3. The Task Assignee or Admin changes the Status of the Task (e.g., from To-Do to In Progress).
4. The Admin or Task Owner changes the Priority Level of the Task to reflect its urgency.
5. The User adds a Comment to the Task to provide updates, ask questions, or leave notes.
6. The Admin or original author deletes a Comment if it is outdated or incorrect.
7. The Task Owner or Admin assigns a Category to the Task for organizational purposes.
8. Admin creates a new Category by providing a Name and Description.
9. The User selects a Category to filter and view all Tasks under that Category.
Postconditions: The system displays a list of Tasks associated with the selected Category.
10. The User views detailed information about a Task, including Description, Due Date, Status, Priority Level, Assignee, Comments, and Category.
11. The Task Owner or Admin modifies the Description, Due Date, Priority Level, or Category of the Task.
12. The Task Assignee or Admin marks the Task as Completed.

- For each domain object create a folder with two subfolders Queries and Commands. For example 
    - Tasks Folder
        - Commands.
            - CreateUnitWork
                - CreateUnitWorkCommand.cs
                - CreateUnitWorkCommandHandler.cs
            - DeleteUnitWork
        - Queries
            - GetUnitWork
                - ListUnitWorkCommand.cs
                - ListUnitWorkCommandHandler.cs


```
using ErrorOr;
using MediatR;
using TaskManagement.Domain.WorkItems;

namespace TaskManagement.Application.WorkItems.Commands.CreateWorkItem;

public record CreateWorkItemCommand(string Name, string Description,
    DateTime DueDate, WorkItemStatus TaskStatus,
    Guid CategoryId, Guid UserAssignedToId) : IRequest<ErrorOr<WorkItem>>;
```

```
using ErrorOr;
using MediatR;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.WorkItems;


namespace TaskManagement.Application.WorkItems.Commands.CreateWorkItem;

public class CreateTaskCommandHandler(IWorkItemsRepository workItemsRepository,
    ICategoriesRepository categoriesRepository,
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork) :
    IRequestHandler<CreateWorkItemCommand, ErrorOr<WorkItem>>
{
    public async Task<ErrorOr<WorkItem>> Handle(
        CreateWorkItemCommand request, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.GetByIdAsync(request.CategoryId);
        var assignedUser = await usersRepository.GetByIdAsync(request.UserAssignedToId);

        if (category is null)
        {
            return WorkItemErrors.CategoryNotFound;
        }
        if (assignedUser is null)
        {
            return WorkItemErrors.AssignedUserNotFound;
        }

        var task = new WorkItem(
            name: request.Name, description: request.Description,
            dueDate: request.DueDate, workItemStatus: request.TaskStatus,
            categoryId: request.CategoryId,
            categoryName: category.Name,
            assignedToId: request.UserAssignedToId,
            assignedToName: assignedUser.Name);

        await workItemsRepository.AddWorkItemAsync(task);
        await unitOfWork.CommitChangesAsync();

        return task;
    }
}
```

### Implementing Repository Pattern
In the **Application Project** create a Common folder

- Common
    - Interfaces
        - IUnitWorkRepository.cs
        - IUnitOfQWork.cs

```
using TaskManagement.Domain.WorkItems;

namespace TaskManagement.Application.Common.Interfaces;

public interface IWorkItemsRepository
{
    Task AddWorkItemAsync(WorkItem workItem);
    Task<WorkItem?> GetByIdAsync(Guid workItemId);
    Task<List<WorkItem>> GetAllAsync();
    Task UpdateWorkItemAsync(WorkItem workItem);
    Task<bool> ExistsAsync(Guid id);
    Task RemoveWorkItemAsync(WorkItem workItem);
    Task<List<WorkItem>> GetWorkItemsByCategoryIdAsync(Guid categoryId);
    Task RemoveRangeAsync(List<WorkItem> workItems);
}
```

```
namespace TaskManagement.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}
```

## 3. Presentation Layer

We have 2 projects in this layer **Contracts Project** and **API Project**.

### 3.1 Contracts

- Create Contracts Project independent to be able to publish to Nuget for the Client. We use this project to have a common language between the **Presentation Layer** and the **Application Layer**

- WorkItems
    - CreateWorkItemRequest.cs
    - WorkItemResponse.cs

- Create class CreateWorkItemRequest.cs

```
namespace TaskManagement.Contracts.WorkItems;

public record CreateWorkItemRequest(
    string Name,
    string Description,
    DateTime DueDate,
    WorkItemStatus WorkItemStatus,
    Guid CategoryId,
    Guid UserAssignedToId);
```

- Create class WorkItemReponse.cs
```
namespace TaskManagement.Contracts.WorkItems;

public record WorkItemResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime DueDate,
    string Status,
    string? CategoryName);

```

### 3.2 API

The API layer will need to use classes from the **Infrastructure Layer** and the **Application Layer** for this we can use **Dependency Injection** and add each class to the Program.cs class or we can create a class DependencyInjection.cs in each Project **(Infrastructure and Application)** and add these 2 classes to Program.cs. In this way the Infrastructure Project and the Application Project are independent of the API.

- Define EndPoints
  - Post Task
  - Post User
  - Post Category
  - Post Comment

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

## 4. Infrastructure Layer

- In this layer we implements all the repositories interfaces that we define in the **Application Layer**
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
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Attachments;
using TaskManagement.Domain.Categories;
using TaskManagement.Domain.Comments;
using TaskManagement.Domain.Common;
using TaskManagement.Domain.Users;
using TaskManagement.Domain.WorkItems;

namespace TaskManagement.Infrastructure.Common.Persistence;

public class TaskManagementDbContext(
    DbContextOptions options,
    IHttpContextAccessor httpContextAccessor,
    IPublisher publisher) : DbContext(options), IUnitOfWork
{
    private readonly IHttpContextAccessor httpContextAccessor = httpContextAccessor;
    private readonly List<AuditEntry> auditEntriesList = [];
    public DbSet<WorkItem> WorkItems { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Attachment> Attachments { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<AuditEntry> AuditEntries { get; set; } = null!;

    public async Task CommitChangesAsync()
    {
        // get hold of all the domain events
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(x => x)
            .ToList();
        // // store them in the http context for later if user is waiting online
        if (IsUserWaitingOnline())
        {
            AddDomainEventsToOfflineProcessingQueue(domainEvents);
        }
        else
        {
            await PublishDomainEvents(publisher, domainEvents);
        }
        await SaveChangesAsync();
    }

    private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
    {
        // fetch queue from http context or create a new queue if it doesn't exist
        var domainEventsQueue = httpContextAccessor.HttpContext!.Items
            .TryGetValue("DomainEventsQueue", out var value) && value is Queue<IDomainEvent> existingDomainEvents
                ? existingDomainEvents
                : new Queue<IDomainEvent>();

        // add the domain events to the end of the queue
        domainEvents.ForEach(domainEventsQueue.Enqueue);

        // store the queue in the http context
        httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditInterceptor(auditEntriesList));
        base.OnConfiguring(optionsBuilder);
    }

    private bool IsUserWaitingOnline() => httpContextAccessor.HttpContext is not null;

    private static async Task PublishDomainEvents(IPublisher _publisher, List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}
```

- Create Tasks --> Persistence Folder
    - WorkItems
        - Persistence
            - WorkItemRepository.cs

- WorkItemRepository.cs

```
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Infrastructure.Common.Persistence;
using TaskManagement.Domain.WorkItems;

namespace TaskManagement.Infrastructure.WorkItems.Persistence;

public class WorkItemsRepository(TaskManagementDbContext dbContext) : IWorkItemsRepository
{
    private readonly TaskManagementDbContext dbContext = dbContext;

    public async Task AddWorkItemAsync(WorkItem workItem) => await dbContext.WorkItems.AddAsync(workItem);

    public Task<bool> ExistsAsync(Guid id) => throw new NotImplementedException();

    public async Task<List<WorkItem>> GetAllAsync() => await dbContext.WorkItems.ToListAsync();

    public async Task<WorkItem?> GetByIdAsync(Guid workItemId) => await dbContext.WorkItems.FirstOrDefaultAsync(workItem => workItem.Id == workItemId);

    public Task UpdateWorkItemAsync(WorkItem workItem)
    {
        dbContext.WorkItems.Update(workItem);
        return Task.CompletedTask;
    }

    Task IWorkItemsRepository.RemoveWorkItemAsync(WorkItem workItem)
    {
        dbContext.WorkItems.Remove(workItem);
        return Task.CompletedTask;
    }

    public async Task<List<WorkItem>> GetWorkItemsByCategoryIdAsync(Guid categoryId) => await dbContext.WorkItems.Where(workItem => workItem.CategoryId == categoryId).ToListAsync();
    public Task RemoveRangeAsync(List<WorkItem> workItems)
    {
        dbContext.RemoveRange(workItems);

        return Task.CompletedTask;
    }
}
```

- Create a file to handle Dependency Injection for the API (DependencyInjection.cs)

```
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Infrastructure.Attachments.Persistence;
using TaskManagement.Infrastructure.Categories.Persistence;
using TaskManagement.Infrastructure.Comments.Persistence;
using TaskManagement.Infrastructure.Common.Persistence;
using TaskManagement.Infrastructure.WorkItems.Persistence;
using TaskManagement.Infrastructure.Users.Persistence;

namespace TaskManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) => services.AddPersistence(configuration);

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ProjectContext");
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
}
```
- To Add **Middleware** for applying Eventual Consistency

```
using TaskManagement.Infrastructure.Common.Middleware;
using Microsoft.AspNetCore.Builder;

namespace TaskManagement.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<EventualConsistencyMiddleware>();

        return builder;
    }
}
```

</details>

## CI/CD implementation (GitHub Actions and Biceps)

<details>
<summary><b>Content</b></summary>

### 1. Setup: PR Verify Workflow

The PR Verify workflow verifies the application on pull requests to the master branch by:
Checking out the repository.
Setting up .NET Core SDK.
Building the application and verifying code format changes.
This PR Verify workflow is well-suited for running lightweight checks before merging to the master branch.

```
name: PR Verify

on:
  pull_request:
    branches: ["master"]

jobs:
  build:
    name: PR Verify
    runs-on: ubuntu-latest

    steps:
      - name: Checkout repo
        uses: actions/checkout@v4

      - name: Set up .NET Core
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: 8.0

      - name: dotnet build
        run: dotnet build --configuration Release

      # - name: dotnet test 1
      #   run: dotnet test --configuration Release --no-build

      - name: dotnet format
        run: dotnet format -v detailed --verify-no-changes

```

### 2. CI Workflow Configuration

This CI workflow runs on every push to the master branch and includes:
Build and Publish: The application is built, and the output is published in a publish folder.
Artifact Upload: Uploads the dotnet-artifact artifact for deployment and generates a SQL migration script for Entity Framework, saved as sql-script-updates.

```
name: CI

on:
  push:
    branches: [master]
  workflow_dispatch:

permissions:
  id-token: write
  pull-requests: write
  contents: read

jobs:
  build_and_test:
    name: CI
    runs-on: ubuntu-latest

    steps:
      - name: Checkout repo
        uses: actions/checkout@v4

        # Set up .NET Core SDK
      - name: Set up .NET Core
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: 8.0

        # Create an artifact in folder publish
      - name: dotnet publish
        run: dotnet publish ./src/TaskManagement.Api/TaskManagement.Api.csproj -c Release -o ./publish

        # Upload the artifact to be used in deployment with Name dotnet-artifact
      - name: Upload artifact
        uses: actions/upload-artifact@v4
        with:
          name: dotnet-artifact
          path: publish/

        # Install EF Core Tools  
      - name: Install EntityFrameworkCore Tools
        run: |
          dotnet new tool-manifest
          dotnet tool install dotnet-ef

        # Generate EF Core Migration Script in folder publish/sql 
      - name: Generate EF Core Migration Script
        run:
          dotnet ef migrations script --idempotent --no-build --configuration Release --output publish/sql/sql-script.sql --context TaskManagementDbContext --project src/TaskManagement.Infrastructure -s src/TaskManagement.Api           

        # Upload the migration script to be used in deployment with Name sql-script-updates
      - uses: actions/upload-artifact@v4
        with:
          name: sql-script-updates
          path: publish/sql/sql-script.sql

  deploy_dev:
    name: Deploy Dev
    needs: build_and_test
    uses: ./.github/workflows/step-deploy.yml
    with:
      env: dev
      artifact_name: dotnet-artifact
      sql_artifact_name: sql-script-updates
      resource_group_name: gr-taskmanagement-dev
      app_service_name: app-taskmanagement-sandropucp-dev
      app_service_slot_name: slot
    secrets: inherit # Note: use GH Environment Secrets if using a Pro/Enterprise version of GH

```

![alt text](/images/CIV1.jpg)

### 3. Azure Deployment via Step-deploy Workflow

The deploy_dev job initiates a deployment to Azure, depending on build_and_test to ensure that the app is ready and artifacts are available.
Here’s a streamlined process for deployment:
App Service Configuration: In Azure, configure the App Service and slots (like dev or slot) based on environment.
Deploy Using GitHub Secrets: Use GitHub Secrets to securely manage Azure credentials (AZURE_CLIENT_ID, AZURE_CLIENT_SECRET, etc.) to avoid exposing sensitive data.

</details>

## Validation (FluentValidation-Mediator Pipeline Behaviour)

<details>
<summary><b>Content</b></summary>

- For validation we use FluentValidation library. We applied validation in the Application layer
- For each command that we want to validate we create a class to validate the command
- After we register all these validations in the DependencyInjection file

- Here is how we define the behavior in the Pipeline using Mediator

```
using ErrorOr;
using FluentValidation;
using MediatR;

namespace TaskManagement.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator = validator;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        var errors = validationResult.Errors
            .ConvertAll(error => Error.Validation(
                code: error.PropertyName,
                description: error.ErrorMessage));

        return (dynamic)errors;
    }
}

```

- Here is an example of how we register all the validations

```
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Common.Behaviors;

namespace TaskManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));

            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
        return services;
    }
}

````

- Here is an example of a command validation

```
using FluentValidation;
using TaskManagement.Application.Comments.Commands.CreateComment;

namespace TaskManagement.Application.Users.Commands.CreateTask;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.CommentText)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50);
        RuleFor(x => x.WorkItemId)
            .NotEmpty();
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}

````

</details>

## Eventual Consistency with Events (Mediator and Middleware)

<details>
<summary><b>Content</b></summary>

- Eventual consistency is a consistency model used in distributed computing to achieve high availability. In this model, updates to a distributed system are propagated to all nodes asynchronously. This means that, while the system will eventually become consistent, it may not be immediately consistent after an update.
- In this project we applied when the user deletes a Category. The system published an event (CategoryDeletedEvent). 

- In DeleteCategoryCommandHandler.cs (Application)

```
user.DeleteCategory(request.CategoryId);
await unitOfWork.CommitChangesAsync(); // FYI: This will publish the domain events
```

- In User.cs (Domain)

```
public void DeleteCategory(Guid categoryId) =>
        _domainEvents.Add(new CategoryDeletedEvent(categoryId));
```

- In TaskManagementDBContext.cs (Infrastruture) - Publish Events using Mediator

```
    public async Task CommitChangesAsync()
    {
        // get hold of all the domain events
        var domainEvents = ChangeTracker.Entries<Entity>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(x => x)
            .ToList();
        // // store them in the http context for later if user is waiting online
        if (IsUserWaitingOnline())
        {
            AddDomainEventsToOfflineProcessingQueue(domainEvents);
        }
        else
        {
            await PublishDomainEvents(publisher, domainEvents);
        }
        await SaveChangesAsync();
    }

    private void AddDomainEventsToOfflineProcessingQueue(List<IDomainEvent> domainEvents)
    {
        // fetch queue from http context or create a new queue if it doesn't exist
        var domainEventsQueue = _httpContextAccessor.HttpContext!.Items
            .TryGetValue("DomainEventsQueue", out var value) && value is Queue<IDomainEvent> existingDomainEvents
                ? existingDomainEvents
                : new Queue<IDomainEvent>();

        // add the domain events to the end of the queue
        domainEvents.ForEach(domainEventsQueue.Enqueue);

        // store the queue in the http context
        _httpContextAccessor.HttpContext!.Items["DomainEventsQueue"] = domainEventsQueue;
    }

    private bool IsUserWaitingOnline() => _httpContextAccessor.HttpContext is not null;

    private static async Task PublishDomainEvents(IPublisher _publisher, List<IDomainEvent> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
```

- At this time the User already got his Response and the rest of the process will be assyncronous

- Here we setup the Middleware to Publish the events after the user got the response. EventualConsistencyMiddleware.cs (Infrastructure-->Middleware)

```
using TaskManagement.Domain.Common;
using TaskManagement.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace TaskManagement.Infrastructure.Common.Middleware;

public class EventualConsistencyMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IPublisher publisher, TaskManagementDbContext dbContext)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync();

        context.Response.OnCompleted(async () =>
        {
            try
            {
                if (context.Items.TryGetValue("DomainEventsQueue", out var value) &&
                    value is Queue<IDomainEvent> domainEventsQueue)
                {
                    while (domainEventsQueue!.TryDequeue(out var domainEvent))
                    {
                        await publisher.Publish(domainEvent);
                    }
                }

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // notify the client that even though they got a good response, the changes didn't take place
                // due to an unexpected error
            }
            finally
            {
                await transaction.DisposeAsync();
            }

        });

        await _next(context);
    }
}

```

- Here we register Middleware. RequestPipeline.cs (Infrastructure)

```
using TaskManagement.Infrastructure.Common.Middleware;
using Microsoft.AspNetCore.Builder;

namespace TaskManagement.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<EventualConsistencyMiddleware>();

        return builder;
    }
}
```

- We Use the notification of Mediator (MediatR.INotificationHandler) to Notify about the published events.

- In CategoryDeletedEventHandler.cs (Application-->Categories-->Events)

```
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Users.Events;
using MediatR;

namespace TaskManagement.Application.Categories.Events;

public class CategoryDeletedEventHandler(
    ICategoriesRepository categoriesRepository,
    IUnitOfWork unitOfWork) : INotificationHandler<CategoryDeletedEvent>
{
    private readonly ICategoriesRepository _categoriesRepository = categoriesRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetByIdAsync(notification.CategoryId) ?? throw new InvalidOperationException();

        await _categoriesRepository.RemoveCategoryAsync(category);
        await _unitOfWork.CommitChangesAsync();
    }
}


```

- In CategoryDeletedEventHandler.cs (Application-->WorkItems-->Events)

```
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Users.Events;
using MediatR;

namespace TaskManagement.Application.Tasks.Events;

public class CategoryDeletedEventHandler(
    IWorkItemsRepository workItemsRepository,
    IUnitOfWork unitOfWork): INotificationHandler<CategoryDeletedEvent>
{
    private readonly IWorkItemsRepository workItemsRepository = workItemsRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;

    public async Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        var workItems = await workItemsRepository.GetWorkItemsByCategoryIdAsync(notification.CategoryId);

        await workItemsRepository.RemoveRangeAsync(workItems);
        await unitOfWork.CommitChangesAsync();
    }
}

```

### Key Points

1. **Asynchronous Updates**: Changes are propagated to other nodes in the background.
2. **Temporary Inconsistency**: There may be a period where different nodes have different data.
3. **Eventual Consistency**: Given enough time, all nodes will converge to the same state.

</details>

## Send Requests to API

<details>
<summary><b>Content</b></summary>

## Manual Testing API (Option 1)

- Create requests folder in Root project
- Create folder (plural) for each domain class
- Create http file CreateTask.http

```
@HostAddress = http://localhost:5205

POST {{HostAddress}}/tasks/
Content-Type: application/json

{
  "Name": "Ta",
  "CategoryId": "97620830-4908-4c50-8b72-0fcfa09da742",
  "DueDate": "2024-12-31",
  "Status": "NotStarted"
}
```

## Manual Testing API (Option 2)

- Install Thundar Client in VS Code
- Under Env Tab 2 variables for local testing and azure testing
- Under Collections Tab create collection for InsertSampleData, Queries and Update
- In each collection create new requests
- We can export or import these files to have a copy

![alt text](/images/ThunderClientV1.jpg)

</details>

## Log Audit Using EF Interceptors

<details>
<summary><b>Content</b></summary>

- To populate an AuditEntry in the context of an AuditInterceptor, I use an intercept the save changes operation and capture the necessary audit information. 

- Here is the **AuditEntry** class to hold the Audit information (Domain)

```csharp
using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.AuditEntries;

public class AuditEntry : Entity
{
    public string Metadata { get; set; } = null!;
    public DateTime StartTimeUtc { get; set; }
    public DateTime EndTimeUtc { get; set; }
    public bool Succeded { get; set; }
    public string ErrorMessage { get; set; } = null!;
}

```

- To**Implement the SaveChangesInterceptor** I created a class that inherits from SaveChangesInterceptor and override the SavingChangesAsync to capture the audit information

- Here is the AuditInterceptor.cs class (nfrastructure-->Common-->Persistence)

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TaskManagement.Domain.AuditEntries;

namespace TaskManagement.Infrastructure.Common.Persistence;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly List<AuditEntry> auditEntriesList;
    public AuditInterceptor() => auditEntriesList = [];
    public AuditInterceptor(List<AuditEntry> auditEntries) => auditEntriesList = auditEntries;

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        var startTime = DateTime.UtcNow;
        var entries = eventData.Context.ChangeTracker
            .Entries()
            .Where(entry => entry.Entity is not AuditEntry
                &&
                entry.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .Select(entry => new AuditEntry
            {
                Id = Guid.NewGuid(),
                Metadata = entry.DebugView.LongView,
                StartTimeUtc = startTime,
                Succeded = true,
                ErrorMessage = string.Empty
            }).ToList();
        if (entries.Count == 0)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        auditEntriesList.AddRange(entries);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        if (auditEntriesList is null)
        {
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }
        var endTime = DateTime.UtcNow;
        foreach (var entry in auditEntriesList)
        {
            entry.EndTimeUtc = endTime;
            entry.Succeded = true;
        }
        if (auditEntriesList.Count > 0 && eventData.Context is not null)
        {
            // Save audit entries to the database
            // Better approah is to write audit entries to a message bus and let another service handle the audit entries
            eventData.Context.Set<AuditEntry>().AddRange(auditEntriesList);
            auditEntriesList.Clear();
            await eventData.Context.SaveChangesAsync(cancellationToken);
        }
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
    public override async void SaveChangesFailed(DbContextErrorEventData eventData)
    {
        if (auditEntriesList is null)
        {
            return;
        }
        var endTime = DateTime.UtcNow;
        foreach (var entry in auditEntriesList)
        {
            entry.EndTimeUtc = endTime;
            entry.Succeded = false;
            entry.ErrorMessage = eventData.Exception.Message;
        }
        if (auditEntriesList.Count > 0 && eventData.Context is not null)
        {
            // Save audit entries to the database
            // Better approah is to write audit entries to a message bus and let another service handle the audit entries
            eventData.Context.Set<AuditEntry>().AddRange(auditEntriesList);
            auditEntriesList.Clear();
            await eventData.Context.SaveChangesAsync();
        }
        return;
    }
}

```

- To **Register the Interceptor**: I user the  TaskManagementDbContext

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.AddInterceptors(new AuditInterceptor(_auditEntriesList));
    base.OnConfiguring(optionsBuilder);
}
```

- Here is the DepencyInjection.cs to add it

```csharp
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) => services.AddPersistence(configuration);

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ProjectContext");
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
}
```

- This setup captures changes to entities and populates the AuditEntry with relevant information such as table name, action, key values, old values, new values, timestamp, and user ID. 

</details>

## TODO

<details>
<summary><b>Content</b></summary>



## Deployment to Azure

### Azure Interface

### Infrastructure As Code (Bicep)

## How to Handle Security

</details>