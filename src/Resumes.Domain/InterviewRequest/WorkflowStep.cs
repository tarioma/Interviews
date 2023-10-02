using Resumes.Domain.Exceptions;

namespace Resumes.Domain.InterviewRequest;

public class WorkflowStep
{
    public string Name { get; private init; }
    public int Order { get; private init; }
    public Guid UserId { get; private init; }
    public Guid RoleId { get; private init; }
    public string Comment { get; private init; }

    public WorkflowStep(string name, int order, Guid userId, Guid roleId, string comment)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        EmptyGuidException.ThrowIfEmpty(userId);
        EmptyGuidException.ThrowIfEmpty(roleId);
        ArgumentException.ThrowIfNullOrEmpty(comment);

        if (order < 0)
        {
            throw new ArgumentException("order не может быть отрицательным");
        }

        Name = name;
        Order = order;
        UserId = userId;
        RoleId = roleId;
        Comment = comment;
    }
}