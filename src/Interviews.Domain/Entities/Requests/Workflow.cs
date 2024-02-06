namespace Interviews.Domain.Entities.Requests;

public record Workflow
{
    private const int MaxNameLength = 100;

    private readonly List<WorkflowStep> _steps;
    
    public Guid WorkflowTemplateId { get; private init; }
    public string Name { get; private init; }
    public IReadOnlyCollection<WorkflowStep> Steps => _steps;

    private Workflow(Guid workflowTemplateId, string name, List<WorkflowStep> steps)
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
        _steps = steps;
    }

    public static Workflow Create(Guid workflowTemplateId, string name)
    {
        var steps = new List<WorkflowStep>();

        return new Workflow(workflowTemplateId, name, steps);
    }
}