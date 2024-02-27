using Ardalis.GuardClauses;
using GuardClauses;

namespace Interviews.Domain.Entities.WorkflowTemplates;

public record WorkflowStepTemplate
{
    public const int MaxNameLength = 100;

    public WorkflowStepTemplate(string name, int order, Guid employeeId, Guid roleId)
    {
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);
        Guard.Against.Negative(order);

        if (employeeId == Guid.Empty && roleId == Guid.Empty)
        {
            throw new ArgumentException($"{nameof(employeeId)} и {nameof(roleId)} не могут одновременно быть пустыми.");
        }

        if (employeeId != Guid.Empty && roleId != Guid.Empty)
        {
            throw new ArgumentException($"Можно назначить только {nameof(employeeId)} или {nameof(roleId)}.");
        }

        Name = name.Trim();
        Order = order;
        EmployeeId = employeeId;
        RoleId = roleId;
    }

    public string Name { get; }
    public int Order { get; }
    public Guid EmployeeId { get; }
    public Guid RoleId { get; }
}