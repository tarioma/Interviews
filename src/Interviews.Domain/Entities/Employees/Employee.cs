using Ardalis.GuardClauses;

namespace Interviews.Domain.Entities.Employees;

public class Employee
{
    internal const int MaxNameLength = 100;

    internal Employee(Guid id, string name, EmailAddress emailAddress, Guid roleId, AuthData authData)
    {
        Guard.Against.Default(id);
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);
        Guard.Against.Null(emailAddress);
        Guard.Against.Default(roleId);
        Guard.Against.Null(authData);

        Id = id;
        Name = name;
        EmailAddress = emailAddress;
        RoleId = roleId;
        AuthData = authData;
    }

    public Guid Id { get; }
    public string Name { get; }
    public EmailAddress EmailAddress { get; }
    public Guid RoleId { get; }
    public AuthData AuthData { get; }

    public static Employee Create(string name, EmailAddress emailAddress, Guid roleId, AuthData authData)
    {
        var id = Guid.NewGuid();

        return new Employee(id, name, emailAddress, roleId, authData);
    }
}