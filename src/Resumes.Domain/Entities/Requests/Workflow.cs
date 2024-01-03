namespace Resumes.Domain.Entities.Requests;

public class Workflow
{
    private readonly HashSet<WorkflowStep> _steps;
    
    public Guid Id { get; }
    public string Name { get; private set; } = null!;
    public Guid WorkflowTemplateId { get; private init; }
    public IReadOnlyCollection<WorkflowStep> Steps => _steps;

    private Workflow(Guid id, string name, Guid workflowTemplateId, HashSet<WorkflowStep> steps)
    {
        Id = id;
        SetName(name);
        WorkflowTemplateId = workflowTemplateId;
        _steps = steps;
    }

    public static Workflow Create(string name, Guid workflowTemplateId)
    {
        var id = Guid.NewGuid();
        var steps = new HashSet<WorkflowStep>();
        return new Workflow(id, name, workflowTemplateId, steps);
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;
    }
    
    public void AddStep(string name, Status status, string? comment, Guid? userId = null, Guid? roleId = null)
    {
        if (userId is null && roleId is null)
        {
            throw new ArgumentException($"{nameof(userId)} и {nameof(roleId)} не могут быть оба null");
        }
        
        if (userId is not null)
        {
            _steps.Add(WorkflowStep.CreateWithUserId(name, status, (Guid)userId, comment));
            return;
        }
        
        _steps.Add(WorkflowStep.CreateWithRoleId(name, status, (Guid)roleId!, comment));
    }

    public void Restart()
    {
        _steps.Clear();
    }
    
    public bool IsApproved()
    {
        return Steps.Any() &&
               Steps.LastOrDefault()!.Status == Status.Approve &&
               !Steps.Any(step => step.Status is Status.Reject or Status.Pending);
    }

    public bool IsRejected()
    {
        return Steps.Any(step => step.Status == Status.Reject) &&
               !Steps.Any(step => step.Status is Status.Approve or Status.Pending);
    }
}