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