using Resumes.Domain.Entities;
using Resumes.Domain.Exceptions;

namespace Resumes.Domain.UserInfo;

public class User
{
    public Guid Id { get; private init; }
    public string Name { get; private set; }
    public EmailAddress EmailAddress { get; private set; }
    public Guid RoleId { get; private set; }

    private User(Guid id, string name, EmailAddress emailAddress, Guid roleId)
    {
        EmptyGuidException.ThrowIfEmpty(id);
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(emailAddress);
        EmptyGuidException.ThrowIfEmpty(roleId);
        
        Id = id;
        Name = name;
        EmailAddress = emailAddress;
        RoleId = roleId;
    }
    
    public static User Create(string name, EmailAddress emailAddress, Guid roleId)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(emailAddress);
        EmptyGuidException.ThrowIfEmpty(roleId);
        
        var id = Guid.NewGuid();
        return new User(id, name, emailAddress, roleId);
    }

    public void UpdateName(string newName)
    {
        ArgumentException.ThrowIfNullOrEmpty(newName);

        Name = newName;
    }
    
    public void UpdateEmailAddress(EmailAddress newEmailAddress)
    {
        ArgumentNullException.ThrowIfNull(newEmailAddress);

        EmailAddress = newEmailAddress;
    }
    
    public void UpdateRoleId(Guid newRoleId)
    {
        EmptyGuidException.ThrowIfEmpty(newRoleId);

        RoleId = newRoleId;
    }
}