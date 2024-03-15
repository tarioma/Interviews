using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;

namespace Interviews.Domain.Entities.Employees;

public class Employee
{
    internal const int MaxNameLength = 100;

    internal Employee(Guid id, string name, EmailAddress emailAddress, Guid roleId)
    {
        Guard.Against.Default(id);
        Guard.Against.Default(roleId);

        Id = id;
        SetName(name);
        SetEmailAddress(emailAddress);
        RoleId = roleId;
    }

    public Guid Id { get; }
    public string Name { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public Guid RoleId { get; }

    public static Employee Create(string name, EmailAddress emailAddress, Guid roleId)
    {
        var id = Guid.NewGuid();

        return new Employee(id, name, emailAddress, roleId);
    }

    [MemberNotNull(nameof(Name))]
    public void SetName(string name)
    {
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);

        Name = name;
    }

    [MemberNotNull(nameof(EmailAddress))]
    public void SetEmailAddress(EmailAddress emailAddress)
    {
        Guard.Against.Null(emailAddress);

        EmailAddress = emailAddress;
    }
}