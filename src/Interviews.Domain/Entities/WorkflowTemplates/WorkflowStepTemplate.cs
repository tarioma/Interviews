namespace Interviews.Domain.Entities.WorkflowTemplates;

public record WorkflowStepTemplate
{
    private const int MaxNameLength = 100;

    public string Name { get; private init; }
    public int Order { get; private init; }
    public Guid? UserId { get; private init; }
    public Guid? RoleId { get; private init; }

    public WorkflowStepTemplate(string name, int order, Guid? userId = null, Guid? roleId = null)
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

        if ((userId is null || userId == Guid.Empty) &&
            (roleId is null || roleId == Guid.Empty))
        {
            throw new ArgumentException(
                $"{nameof(userId)} и {nameof(roleId)} не могут одновременно быть пустыми или null.");
        }

        if (userId is null || userId == Guid.Empty)
        {
            RoleId = roleId;
        }
        else
        {
            UserId = userId;
        }

        Name = name.Trim();
        Order = order;
    }
}