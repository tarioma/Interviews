using Resumes.Domain.Entities.Exceptions;

namespace Resumes.Domain.Entities;

public class User
{
    public Guid Id { get; private init; }
    public string Name { get; private set; } = null!;
    public EmailAddress EmailAddress { get; private set; } = null!;
    public Guid RoleId { get; private set; }

    private User(Guid id, string name, EmailAddress emailAddress, Guid roleId)
    {
        EmptyGuidException.ThrowIfEmpty(id);
        
        Id = id;
        SetName(name);
        SetEmailAddress(emailAddress);
        SetRoleId(roleId);
    }
    
    public static User Create(string name, EmailAddress emailAddress, Guid roleId)
    {
        var id = Guid.NewGuid();
        return new User(id, name, emailAddress, roleId);
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;
    }
    
    public void SetEmailAddress(EmailAddress emailAddress)
    {
        ArgumentNullException.ThrowIfNull(emailAddress);
        EmailAddress = emailAddress;
    }
    
    public void SetRoleId(Guid roleId)
    {
        EmptyGuidException.ThrowIfEmpty(roleId);
        RoleId = roleId;
    }
}