using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Domain.Entities.Requests;

public record Workflow
{
    private const int MaxNameLength = 100;

    private readonly List<WorkflowStep> _steps;
    
    public Guid WorkflowTemplateId { get; private init; }
    public string Name { get; private init; }
    public IReadOnlyCollection<WorkflowStep> Steps => _steps;

    private Workflow(Guid workflowTemplateId, string name, IEnumerable<WorkflowStep> steps)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(steps);
        
        if (workflowTemplateId == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(workflowTemplateId));
        }
        
        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxNameLength} символов.", nameof(name));
        }

        WorkflowTemplateId = workflowTemplateId;
        Name = name;
        _steps = steps.ToList();
    }

    public static Workflow Create(WorkflowTemplate workflowTemplate)
    {
        ArgumentNullException.ThrowIfNull(workflowTemplate);
        ArgumentNullException.ThrowIfNull(workflowTemplate.Steps);

        if (workflowTemplate.Steps.Count == 0)
        {
            throw new ArgumentException("Список шагов не может быть пустым.", nameof(workflowTemplate));
        }
        
        var workflowTemplateId = workflowTemplate.Id;
        var name = workflowTemplate.Name;
        var steps = workflowTemplate.Steps.Select(s => WorkflowStep.Create(s));

        return new Workflow(workflowTemplateId, name, steps);
    }

    internal bool IsApproved() => Steps.All(s => s.Status == Status.Approved);
    
    internal bool IsRejected() => Steps.Any(s => s.Status == Status.Rejected);
    
    internal void Approve(Employee employee, string? comment = null)
    {
        CheckTerminalState();

        var lastStep = GetLastStep();
        VerifyRights(employee, lastStep);
        
        lastStep.SetStatus(Status.Approved);
        lastStep.SetComment(comment);
    }

    internal void Reject(Employee employee, string? comment = null)
    {
        CheckTerminalState();
        
        var lastStep = GetLastStep();
        VerifyRights(employee, lastStep);
        
        lastStep.SetStatus(Status.Rejected);
        lastStep.SetComment(comment);
    }
    
    internal void Restart()
    {
        foreach (var step in _steps)
        {
            step.SetStatus(Status.Pending);
            step.SetComment(null);
        }
    }
    
    private void CheckTerminalState()
    {
        if (IsApproved())
        {
            throw new Exception("Уже одобрено.");
        }
        
        if (IsRejected())
        {
            throw new Exception("Уже отклонено.");
        }
    }
    
    private WorkflowStep GetLastStep()
    {
        var lastStep = Steps.Where(s => s.Status == Status.Pending).MinBy(s => s.Order);

        if (lastStep is null)
        {
            throw new Exception("Нет шага со статусом ожидания.");
        }
        
        return lastStep;
    }
    
    private static void VerifyRights(Employee employee, WorkflowStep workflowStep)
    {
        if (employee.Id != workflowStep.EmployeeId && employee.RoleId != workflowStep.RoleId)
        {
            throw new ArgumentException("Пользователь не может изменить статус данного шага.", nameof(employee));
        }
    }
}