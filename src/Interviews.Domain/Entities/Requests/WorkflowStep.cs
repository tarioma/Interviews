using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Domain.Entities.Requests;

public class WorkflowStep
{
    private const int MaxNameLength = 100;
    private const int MaxCommentLength = 500;

    public string Name { get; private init; }
    public int Order { get; private init; }
    public Guid EmployeeId { get; private init; }
    public Guid RoleId { get; private init; }
    public Status Status { get; private set; }
    public string? Comment { get; private set; }

    private WorkflowStep(string name, int order, Guid employeeId, Guid roleId, Status status, string? comment = null)
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
        if (status == Status.None)
        {
            throw new ArgumentException("Не может иметь значение по умолчанию.", nameof(status));
        }

        Status = status;
    }

    private void SetComment(string? comment)
    {
        if (comment is null)
        {
            Comment = null;
        }

        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new ArgumentException("Не может быть пустой областью.", nameof(comment));
        }
        
        if (comment.Length > MaxCommentLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxCommentLength} символов.", nameof(comment));
        }

        Comment = comment;
    }
}