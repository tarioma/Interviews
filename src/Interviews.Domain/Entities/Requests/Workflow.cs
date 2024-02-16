using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Domain.Entities.Requests;

public record Workflow
{
    public const int MaxNameLength = 100;
    
    public Guid WorkflowTemplateId { get; private init; }
    public string Name { get; private init; }
    public IReadOnlyCollection<WorkflowStep> Steps { get; private init; }

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

        var stepsList = steps.ToList();
        ValidateSteps(stepsList);

        WorkflowTemplateId = workflowTemplateId;
        Name = name;
        Steps = stepsList;
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

        var activeStep = GetActiveStep();
        activeStep.Approve(employee, comment);
    }

    internal void Reject(Employee employee, string? comment = null)
    {
        CheckTerminalState();
        
        var activeStep = GetActiveStep();
        activeStep.Reject(employee, comment);
    }
    
    internal void Restart()
    {
        foreach (var step in Steps)
        {
            step.ToPending();
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
    
    private WorkflowStep GetActiveStep()
    {
        var activeStep = Steps.Where(s => s.Status == Status.Pending).MinBy(s => s.Order);

        if (activeStep is null)
        {
            throw new Exception("Нет шага со статусом ожидания.");
        }
        
        return activeStep;
    }
    
    private static void ValidateSteps(List<WorkflowStep> steps)
    {
        ArgumentNullException.ThrowIfNull(steps);

        if (steps.Count == 0)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(steps));
        }
        
        // Являются ли номера шагов уникальной возрастающей порядковой последовательностью от 0 до их количества 
        var isCorrectOrder = steps
            .OrderBy(s => s.Order)
            .Select(s => s.Order)
            .SequenceEqual(Enumerable.Range(0, steps.Count));
        
        if (!isCorrectOrder)
        {
            throw new ArgumentException("Номера шагов должны быть уникальной порядковой последовательностью от 0.",
                nameof(steps));
        }
    }
}