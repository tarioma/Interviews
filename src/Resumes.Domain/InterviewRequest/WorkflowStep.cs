namespace Resumes.Domain.InterviewRequest;

public class WorkflowStep
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public int Order { get; private set; }
    public Status Status { get; set; }
    public Guid? UserId { get; private set; }
    public Guid? RoleId { get; private set; }
    public string? Comment { get; set; }

    private WorkflowStep(
        Guid id,
        string name,
        int order,
        Status status,
        string? comment,
        Guid? userId = null,
        Guid? roleId = null)
    {
        if (userId is null && roleId is null)
        {
            throw new ArgumentException($"{nameof(userId)} и {nameof(roleId)} не могут быть оба отрицательным");
        }

        Id = id;
        SetName(name);
        SetOrder(order);
        Status = status;
        UserId = userId;
        RoleId = roleId;
        Comment = comment;
    }

    public static WorkflowStep CreateWithUserId(string name, int order, Status status, Guid userId, string? comment)
    {
        var id = Guid.NewGuid();
        return new WorkflowStep(id, name, order, status, comment, userId: userId);
    }
    
    public static WorkflowStep CreateWithRoleId(string name, int order, Status status, Guid roleId, string? comment)
    {
        var id = Guid.NewGuid();
        return new WorkflowStep(id, name, order, status, comment, roleId: roleId);
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;
    }

    public void SetOrder(int order)
    {
        if (order < 0)
        {
            throw new ArgumentException($"{nameof(order)} не может быть отрицательным");
        }

        Order = order;
    }
}