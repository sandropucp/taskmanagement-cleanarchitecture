using TaskManagement.Application.SubcutaneousTests.Common;

namespace TaskManagement.Application.SubcutaneousTests.Common;

[CollectionDefinition(CollectionName)]
public class MediatorFactoryCollection : ICollectionFixture<MediatorFactory>
{
    public const string CollectionName = "MediatorFactoryCollection";
}