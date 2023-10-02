using Resumes.Domain.Exceptions;

namespace Resumes.Domain.UserInfo;

public class Role
{
    public Guid Id { get; private init; }
    public string Name { get; private set; }

    private Role(Guid id, string name)
    {
        EmptyGuidException.ThrowIfEmpty(id);
        ArgumentException.ThrowIfNullOrEmpty(name);
        
        Id = id;
        Name = name;
    }

    public static Role Create(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        
        var id = Guid.NewGuid();
        return new Role(id, name);
    }

    public void UpdateName(string newName)
    {
        ArgumentException.ThrowIfNullOrEmpty(newName);

        Name = newName;
    }
}