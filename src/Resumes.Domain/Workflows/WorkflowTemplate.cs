using Resumes.Domain.Exceptions;

namespace Resumes.Domain.Workflows;

public class WorkflowTemplate
{
    private HashSet<WorkflowStepTemplate> _steps;
    
    public Guid Id { get; private init; }
    public string Name { get; private init; }
    public ICollection<WorkflowStepTemplate> Steps => _steps;

    private WorkflowTemplate(Guid id, string name, HashSet<WorkflowStepTemplate> steps)
    {
        EmptyGuidException.ThrowIfEmpty(id);
        ArgumentException.ThrowIfNullOrEmpty(name);
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
}