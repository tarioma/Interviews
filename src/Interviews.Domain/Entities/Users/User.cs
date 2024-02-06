namespace Interviews.Domain.Entities.Users;

public class User
{
    private const int MaxNameLength = 100;
    
    public Guid Id { get; private init; }
    public string Name { get; private set; } = null!;
    public EmailAddress EmailAddress { get; private set; } = null!;
    public Guid RoleId { get; private set; }

    private User(Guid id, string name, EmailAddress emailAddress, Guid roleId)
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
    
    public User Create(string name, EmailAddress emailAddress, Guid roleId)
    {
        var id = Guid.NewGuid();
        
        return new User(id, name, emailAddress, roleId);
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxNameLength} символов.", nameof(name));
        }
        
        Name = name.Trim();
    }
    
    public void SetEmailAddress(EmailAddress emailAddress)
    {
        ArgumentNullException.ThrowIfNull(emailAddress);
        
        EmailAddress = emailAddress;
    }
    
    public void SetRoleId(Guid roleId)
    {
        if (roleId == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(roleId));
        }

        RoleId = roleId;
    }
}