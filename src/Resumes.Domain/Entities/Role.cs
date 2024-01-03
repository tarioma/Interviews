using Resumes.Domain.Entities.Exceptions;

namespace Resumes.Domain.Entities;

public class Role
{
    public Guid Id { get; private init; }
    public string Name { get; private set; } = null!;

    private Role(Guid id, string name)
    {
        EmptyGuidException.ThrowIfEmpty(id);
        
        Id = id;
        SetName(name);
    }

    public static Role Create(string name)
    {
        var id = Guid.NewGuid();
        return new Role(id, name);
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;
    }
}