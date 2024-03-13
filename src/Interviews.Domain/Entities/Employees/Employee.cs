using Ardalis.GuardClauses;

namespace Interviews.Domain.Entities.Employees;

public class Employee
{
    internal const int MaxNameLength = 100;

    internal Employee(Guid id, string name, EmailAddress emailAddress, Guid roleId)
    {
        Guard.Against.Default(id);
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);
        Guard.Against.Null(emailAddress);
        Guard.Against.Default(roleId);

        Id = id;
        Name = name;
        EmailAddress = emailAddress;
        RoleId = roleId;
    }

    public Guid Id { get; }
    public string Name { get; }
    public EmailAddress EmailAddress { get; }
    public Guid RoleId { get; }

    public static Employee Create(string name, EmailAddress emailAddress, Guid roleId)
    {
        var id = Guid.NewGuid();

        return new Employee(id, name, emailAddress, roleId);
    }
}