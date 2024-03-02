using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;

namespace Interviews.Domain.Entities.Employees;

public class Role
{
    internal const int MaxNameLength = 100;

    public Role(Guid id, string name)
    {
        Guard.Against.Default(id);

        Id = id;
        SetName(name);
    }

    public Guid Id { get; }
    public string Name { get; private set; }

    public static Role Create(string name)
    {
        var id = Guid.NewGuid();

        return new Role(id, name);
    }

    [MemberNotNull(nameof(Name))]
    private void SetName(string name)
    {
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);

        Name = name.Trim();
    }
}