using Resumes.Domain.Entities.Exceptions;
using Resumes.Domain.Entities.Requests;

namespace Resumes.Domain.Entities.Templates;

public class WorkflowTemplate
{
    private HashSet<WorkflowStepTemplate> _steps;
    
    public Guid Id { get; private init; }
    public string Name { get; private set; }
    public ICollection<WorkflowStepTemplate> Steps => _steps;

    private WorkflowTemplate(Guid id, string name, HashSet<WorkflowStepTemplate> steps)
    {
        EmptyGuidException.ThrowIfEmpty(id);
        ArgumentNullException.ThrowIfNull(steps);
        
        Id = id;
        Name = name;
        _steps = steps;
    }

    public static WorkflowTemplate Create(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        
        var id = Guid.NewGuid();
        var steps = new HashSet<WorkflowStepTemplate>();
        return new WorkflowTemplate(id, name, steps);
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;
    }
    
    public Request CreateRequest(User user, Document document)
    {
        return Request.Create(user, document, Workflow.Create(Name, Id));
    }
}