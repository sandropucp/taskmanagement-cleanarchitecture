namespace TaskManagement.Api.IntegrationTests.Common;

[CollectionDefinition(CollectionName)]
public class TaskManagementApiFactoryCollection : ICollectionFixture<TaskManagementApiFactory>
{
    public const string CollectionName = "TaskManagementApiFactoryCollection";
}
