namespace Interviews.Domain.Entities.Requests;

public record WorkflowStep
{
    private const int MaxNameLength = 100;
    private const int MaxCommentLength = 500;
    
    public string Name { get; private init; }
    public int Order { get; private init; }
    public string Comment { get; private init; }
    public Guid? UserId { get; private init; }
    public Guid? RoleId { get; private init; }
    
    public WorkflowStep(string name, int order, string comment, Guid? userId = null, Guid? roleId = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(comment);
        
        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxNameLength} символов.", nameof(name));
        }
        
        if (comment.Length > MaxCommentLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxCommentLength} символов.", nameof(comment));
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
        Comment = comment.Trim();
        UserId = userId;
        RoleId = roleId;
    }

    public void SetStatus()
    {
        // TODO
    }
}