using Resumes.Domain.Exceptions;

namespace Resumes.Domain.InterviewRequest;

public class Workflow
{
    private readonly HashSet<WorkflowStep> _steps;
    
    public Guid Id { get; }
    public string Name { get; private set; }
    public Guid WorkflowTemplateId { get; private set; }
    public IReadOnlyCollection<WorkflowStep> Steps => _steps;

    private Workflow(Guid id, string name, Guid workflowTemplateId, HashSet<WorkflowStep> steps)
    {
        Id = id;
        WorkflowTemplateId = workflowTemplateId;
        Name = name;
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
    
    public void SetWorkflowTemplateId(Guid workflowTemplateId)
    {
        EmptyGuidException.ThrowIfEmpty(workflowTemplateId);
        WorkflowTemplateId = workflowTemplateId;
    }
}