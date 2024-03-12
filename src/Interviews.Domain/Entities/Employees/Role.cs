using Ardalis.GuardClauses;

namespace Interviews.Domain.Entities.Employees;

public class Role
{
    internal const int MaxNameLength = 100;

    internal Role(Guid id, string name)
    {
        Guard.Against.Default(id);
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);

        Id = id;
        Name = name.Trim();
    }

    public Guid Id { get; }
    public string Name { get; }

    public static Role Create(string name)
    {
        var id = Guid.NewGuid();

        return new Role(id, name);
    }
}