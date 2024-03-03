using Ardalis.GuardClauses;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Domain.Entities.Requests;

public class WorkflowStep
{
    internal const int MaxNameLength = 100;
    internal const int MaxCommentLength = 500;

    public WorkflowStep(
        string name,
        int order,
        Status status,
        Guid? employeeId = null,
        Guid? roleId = null,
        string? comment = null)
    {
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);
        Guard.Against.Negative(order);

        var employeeIdIsDefined = employeeId is null || employeeId == Guid.Empty;
        var roleIdIsDefined = roleId is null || roleId == Guid.Empty;

        if (!(employeeIdIsDefined || roleIdIsDefined))
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
        SetStatus(status);
        SetComment(comment);
    }

    public string Name { get; }
    public int Order { get; }
    public Status Status { get; private set; }
    public Guid? EmployeeId { get; }
    public Guid? RoleId { get; }
    public string? Comment { get; private set; }

    public static WorkflowStep Create(WorkflowStepTemplate workflowStepTemplate, string? comment = null)
    {
        Guard.Against.Null(workflowStepTemplate);

        var name = workflowStepTemplate.Name;
        var order = workflowStepTemplate.Order;
        var employeeId = workflowStepTemplate.EmployeeId;
        var roleId = workflowStepTemplate.RoleId;
        var status = Status.Pending;

        return new WorkflowStep(name, order, status, employeeId, roleId, comment);
    }

    internal void Approve(Employee employee, string? comment = null)
    {
        Guard.Against.Null(employee);

        VerifyRights(employee);

        SetStatus(Status.Approved);
        SetComment(comment);
    }

    internal void Reject(Employee employee, string? comment = null)
    {
        Guard.Against.Null(employee);

        VerifyRights(employee);

        SetStatus(Status.Rejected);
        SetComment(comment);
    }

    internal void ToPending()
    {
        SetStatus(Status.Pending);
        SetComment(null);
    }

    private void VerifyRights(Employee employee)
    {
        if (employee.Id != EmployeeId && employee.RoleId != RoleId)
        {
            throw new ArgumentException("Пользователь не может изменить статус данного шага.", nameof(employee));
        }
    }

    private void SetStatus(Status status)
    {
        Guard.Against.Default(status);

        Status = status;
    }

    private void SetComment(string? comment)
    {
        if (comment is null)
        {
            Comment = null;
            return;
        }

        Guard.Against.NullOrWhiteSpace(comment);
        Guard.Against.StringTooLong(comment, MaxCommentLength);

        Comment = comment;
    }
}