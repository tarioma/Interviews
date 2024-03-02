using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;

namespace Interviews.Domain.Entities.Employees;

public class Employee
{
    internal const int MaxNameLength = 100;

    public Employee(Guid id, string name, EmailAddress emailAddress, Guid roleId)
    {
        Guard.Against.Default(id);

        Id = id;
        SetName(name);
        SetEmailAddress(emailAddress);
        SetRoleId(roleId);
    }

    public Guid Id { get; }
    public string Name { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public Guid RoleId { get; private set; }

    public static Employee Create(string name, EmailAddress emailAddress, Guid roleId)
    {
        var id = Guid.NewGuid();

        return new Employee(id, name, emailAddress, roleId);
    }

    [MemberNotNull(nameof(Name))]
    private void SetName(string name)
    {
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);

        Name = name.Trim();
    }

    [MemberNotNull(nameof(EmailAddress))]
    private void SetEmailAddress(EmailAddress emailAddress)
    {
        Guard.Against.Null(emailAddress);

        EmailAddress = emailAddress;
    }

    private void SetRoleId(Guid roleId)
    {
        Guard.Against.Default(roleId);

        RoleId = roleId;
    }
}