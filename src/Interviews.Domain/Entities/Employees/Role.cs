using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using GuardClauses;
using Interviews.Domain.Tools;

namespace Interviews.Domain.Entities.Employees;

public class Role
{
    public const int MaxNameLength = 100;
    
    public Guid Id { get; private init; }
    public string Name { get; private set; }

    public Role(Guid id, string name)
    {
        Guard.Against.GuidIsEmpty(id);

        Id = id;
        SetName(name);
    }

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