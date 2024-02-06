namespace Interviews.Domain.Entities.WorkflowTemplates;

public record StepTemplate
{
    private const int MaxNameLength = 100;
    
    public string Name { get; private init; }
    public int Order { get; private init; }
    public Guid? UserId { get; private init; }
    public Guid? RoleId { get; private init; }

    public StepTemplate(string name, int order, Guid? userId = null, Guid? roleId = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxNameLength} символов.", nameof(name));
        }
        
        if (order < 0)
        {
            throw new ArgumentException("Не может быть отрицательным.", nameof(order));
        }
        
        if (userId is null && roleId is null)
        {
            throw new ArgumentException($"{nameof(userId)} и {nameof(roleId)} не могут одновременно быть null.");
        }
        
        if (userId is not null && userId == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(userId));
        }
        
        if (roleId is not null && roleId == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(userId));
        }

        Name = name.Trim();
        Order = order;
        UserId = userId;
        RoleId = roleId;
    }
}