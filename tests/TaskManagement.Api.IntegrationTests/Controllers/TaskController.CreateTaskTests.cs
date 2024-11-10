using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using TaskManagement.Api.IntegrationTests.Common;
using TaskManagement.Contracts.WorkItems;


namespace TaskManagement.Api.IntegrationTests.Controllers;

[Collection(TaskManagementApiFactoryCollection.CollectionName)]

public class CreateTaskTests
{
    private readonly HttpClient _client;

    public CreateTaskTests(TaskManagementApiFactory apiFactory)
    {
        _client = apiFactory.HttpClient;
        apiFactory.ResetDatabase();
    }

    public async Task CreateTask_WhenValidTask_ShouldCreateTask()
    {
        // Arrange
        var createTaskRequest = new CreateWorkItemRequest(
            Name: "Task 1",
            Description: "Task 1 description",
            DueDate: DateTime.UtcNow.AddDays(1),
            WorkItemStatus: WorkItemStatus.InProgress,
            CategoryId: Guid.NewGuid(),
            UserAssignedToId: Guid.NewGuid());

        // Act
        var response = await _client.PostAsJsonAsync("Tasks", createTaskRequest);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var taskResponse = await response.Content.ReadFromJsonAsync<WorkItemResponse>();
        taskResponse.Should().NotBeNull();
        //taskResponse!.SubscriptionType.Should().Be(subscriptionType);

        response.Headers.Location!.PathAndQuery.Should().Be($"/Tasks/{taskResponse!.Id}");
    }
}
