namespace Interviews.Domain.Entities.Users;

public class Role
{
    private const int MaxNameLength = 100;
    
    public Guid Id { get; private init; }
    public string Name { get; private set; } = null!;

    private Role(Guid id, string name)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(id));
        }

        Id = id;
        SetName(name);
    }

    public Role Create(string name)
    {
        var id = Guid.NewGuid();

        return new Role(id, name);
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxNameLength} символов.", nameof(name));   
        }

        Name = name.Trim();
    }
}