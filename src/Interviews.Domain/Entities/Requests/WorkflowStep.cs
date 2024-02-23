using Ardalis.GuardClauses;
using GuardClauses;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.WorkflowTemplates;
using Interviews.Domain.Tools;

namespace Interviews.Domain.Entities.Requests;

public class WorkflowStep
{
    public const int MaxNameLength = 100;
    public const int MaxCommentLength = 500;

    public string Name { get; private init; }
    public int Order { get; private init; }
    public Guid EmployeeId { get; private init; }
    public Guid RoleId { get; private init; }
    public Status Status { get; private set; }
    public string? Comment { get; private set; }

    public WorkflowStep(string name, int order, Guid employeeId, Guid roleId, Status status, string? comment = null)
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

        Name = name;
        Order = order;
        EmployeeId = employeeId;
        RoleId = roleId;
        SetComment(comment);
        SetStatus(status);
    }

    public static WorkflowStep Create(WorkflowStepTemplate workflowStepTemplate, string? comment = null)
    {
        var name = workflowStepTemplate.Name;
        var order = workflowStepTemplate.Order;
        var employeeId = workflowStepTemplate.EmployeeId;
        var roleId = workflowStepTemplate.RoleId;
        var status = Status.Pending;

        return new WorkflowStep(name, order, employeeId, roleId, status, comment);
    }

    internal void Approve(Employee employee, string? comment = null)
    {
        VerifyRights(employee);
        
        SetStatus(Status.Approved);
        SetComment(comment);
    }
    
    internal void Reject(Employee employee, string? comment = null)
    {
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
        Guard.Against.EnumWithDefaultValue(status);

        Status = status;
    }

    private void SetComment(string? comment)
    {
        if (comment is null)
        {
            Comment = null;
        }

        Guard.Against.NullOrWhiteSpace(comment);
        Guard.Against.StringTooLong(comment, MaxNameLength);

        Comment = comment;
    }
}