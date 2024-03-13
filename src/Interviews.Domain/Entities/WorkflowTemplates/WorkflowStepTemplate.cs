using Ardalis.GuardClauses;

namespace Interviews.Domain.Entities.WorkflowTemplates;

public class WorkflowStepTemplate
{
    internal const int MaxNameLength = 100;

    public WorkflowStepTemplate(string name, int order, Guid? employeeId = null, Guid? roleId = null)
    {
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);
        Guard.Against.Negative(order);

        var employeeIdIsDefined = employeeId is null || employeeId == Guid.Empty;
        var roleIdIsDefined = roleId is null || roleId == Guid.Empty;

        if (!employeeIdIsDefined && !roleIdIsDefined)
        {
            throw new ArgumentException($"Один из параметров {nameof(employeeId)} или {nameof(roleId)} обязательный.");
        }

        if (employeeIdIsDefined && roleIdIsDefined)
        {
            throw new ArgumentException($"Можно назначить только {nameof(employeeId)} или {nameof(roleId)}.");
        }

        Name = name;
        Order = order;
        EmployeeId = employeeId;
        RoleId = roleId;
    }

    public string Name { get; }
    public int Order { get; }
    public Guid? EmployeeId { get; }
    public Guid? RoleId { get; }
}