namespace Resumes.Domain.Entities.Requests;

public class WorkflowStep
{
    public Guid Id { get; }
    public string Name { get; private set; } = null!;
    public int Order { get; private set; } = 0;
    public Status Status { get; set; }
    public Guid? UserId { get; private set; }
    public Guid? RoleId { get; private set; }
    public string? Comment { get; set; }

    private WorkflowStep(
        Guid id,
        string name,
        Status status,
        string? comment,
        Guid? userId = null,
        Guid? roleId = null)
    {
        if (userId is null && roleId is null)
        {
            throw new ArgumentException($"{nameof(userId)} и {nameof(roleId)} не могут быть оба null");
        }

        Id = id;
        SetName(name);
        IncrementOrder();
        Status = status;
        UserId = userId;
        RoleId = roleId;
        Comment = comment;
    }

    public static WorkflowStep CreateWithUserId(string name, Status status, Guid userId, string? comment)
    {
        var id = Guid.NewGuid();
        return new WorkflowStep(id, name, status, comment, userId: userId);
    }
    
    public static WorkflowStep CreateWithRoleId(string name, Status status, Guid roleId, string? comment)
    {
        var id = Guid.NewGuid();
        return new WorkflowStep(id, name, status, comment, roleId: roleId);
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;
    }

    public void IncrementOrder()
    {
        Order++;
    }
}