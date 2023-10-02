using Resumes.Domain.Exceptions;

namespace Resumes.Domain.Workflow;

public class WorkflowStepTemplate
{
    public string Name { get; private set; }
    public int Order { get; private set; }
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    public WorkflowStepTemplate(string name, int order, Guid userId, Guid roleId)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        EmptyGuidException.ThrowIfEmpty(userId);
        EmptyGuidException.ThrowIfEmpty(roleId);
        
        if (order < 0)
        {
            throw new ArgumentException("order не может быть отрицательным");
        }
        
        Name = name;
        Order = order;
        UserId = userId;
        RoleId = roleId;
    }

    public void UpdateName(string newName)
    {
        ArgumentException.ThrowIfNullOrEmpty(newName);

        Name = newName;
    }
    
    public void UpdateOrder(int newOrder)
    {
        if (newOrder < 0)
        {
            throw new ArgumentException("order не может быть отрицательным");
        }
        
        Order = newOrder;
    }
    
    public void UpdateUserId(Guid newUserId)
    {
        EmptyGuidException.ThrowIfEmpty(newUserId);

        UserId = newUserId;
    }
    
    public void UpdateRoleId(Guid newRoleId)
    {
        EmptyGuidException.ThrowIfEmpty(newRoleId);

        RoleId = newRoleId;
    }
}