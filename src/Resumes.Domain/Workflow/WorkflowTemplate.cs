using Resumes.Domain.Exceptions;
using Resumes.Domain.InterviewRequest;

namespace Resumes.Domain.Workflow;

public class WorkflowTemplate
{
    public Guid Id { get; private init; }
    public string Name { get; private init; }
    public ICollection<WorkflowStepTemplate> Steps { get; private init; }

    private WorkflowTemplate(Guid id, string name, ICollection<WorkflowStepTemplate> steps)
    {
        EmptyGuidException.ThrowIfEmpty(id);
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentNullException.ThrowIfNull(steps);
        
        Id = id;
        Name = name;
        Steps = steps;
    }

    public static WorkflowTemplate Create(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        
        var id = Guid.NewGuid();
        var steps = new HashSet<WorkflowStepTemplate>();
        return new WorkflowTemplate(id, name, steps);
    }
    
    public static Request CreateRequest(Guid userId, Document document, WorkflowRequest workflowRequest)
    {
        EmptyGuidException.ThrowIfEmpty(userId);
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(workflowRequest);

        return Request.Create(userId, document, workflowRequest);
    }
}