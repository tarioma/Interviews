using Ardalis.GuardClauses;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.WorkflowTemplates;

namespace Interviews.Domain.Entities.Requests;

public class Workflow
{
    internal const int MaxNameLength = 100;

    public Workflow(Guid workflowTemplateId, string name, IReadOnlyCollection<WorkflowStep> steps)
    {
        Guard.Against.Default(workflowTemplateId);
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);
        Guard.Against.NullOrEmpty(steps);

        WorkflowTemplateId = workflowTemplateId;
        Name = name;
        Steps = steps;
    }

    public Guid WorkflowTemplateId { get; }
    public string Name { get; }
    public IReadOnlyCollection<WorkflowStep> Steps { get; }

    public static Workflow Create(WorkflowTemplate workflowTemplate)
    {
        Guard.Against.Null(workflowTemplate);

        var workflowTemplateId = workflowTemplate.Id;
        var name = workflowTemplate.Name;
        var steps = workflowTemplate.Steps.Select(s => WorkflowStep.Create(s)).ToArray();

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
            throw new AggregateException("Нет шага со статусом ожидания.");
        }

        return activeStep;
    }
}