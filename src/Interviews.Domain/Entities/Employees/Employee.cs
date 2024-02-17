using System.Diagnostics.CodeAnalysis;

namespace Interviews.Domain.Entities.Employees;

public class Employee
{
    public const int MaxNameLength = 100;
    
    public Guid Id { get; private init; }
    public string Name { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public Guid RoleId { get; private set; }

    public Employee(Guid id, string name, EmailAddress emailAddress, Guid roleId)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(id));
        }

        Id = id;
        SetName(name);
        SetEmailAddress(emailAddress);
        SetRoleId(roleId);
    }
    
    public static Employee Create(string name, EmailAddress emailAddress, Guid roleId)
    {
        var id = Guid.NewGuid();
        
        return new Employee(id, name, emailAddress, roleId);
    }

    [MemberNotNull(nameof(Name))]
    private void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxNameLength} символов.", nameof(name));
        }
        
        Name = name.Trim();
    }
    
    [MemberNotNull(nameof(EmailAddress))]
    private void SetEmailAddress(EmailAddress emailAddress)
    {
        ArgumentNullException.ThrowIfNull(emailAddress);
        
        EmailAddress = emailAddress;
    }
    
    private void SetRoleId(Guid roleId)
    {
        if (roleId == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(roleId));
        }

        RoleId = roleId;
    }
}