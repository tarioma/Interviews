using Resumes.Domain.Entities.Exceptions;

namespace Resumes.Domain.Entities.Templates;

public class WorkflowStepTemplate
{
    public string Name { get; private init; }
    public int Order { get; private init; }
    public Guid UserId { get; private init; }
    public Guid RoleId { get; private init; }

    private WorkflowStepTemplate(string name, int order, Guid userId, Guid roleId)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        EmptyGuidException.ThrowIfEmpty(userId);
        EmptyGuidException.ThrowIfEmpty(roleId);

        if (order < 0)
        {
            throw new ArgumentOutOfRangeException($"{nameof(order)} не может быть отрицательным");
        }

        Name = name;
        Order = order;
        UserId = userId;
        RoleId = roleId;
    }

    public static WorkflowStepTemplate Create(string name, int order, Guid userId, Guid roleId)
    {
        return new WorkflowStepTemplate(name, order, userId, roleId);
    }
}