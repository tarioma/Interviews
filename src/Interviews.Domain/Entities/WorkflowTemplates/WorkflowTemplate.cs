namespace Interviews.Domain.Entities.WorkflowTemplates;

public class WorkflowTemplate
{
    private const int MaxNameLength = 100;
    
    private List<StepTemplate> _steps;
    
    public Guid Id { get; private init; }
    public string Name { get; private set; } = null!;
    public IReadOnlyCollection<StepTemplate> Steps => _steps;

    private WorkflowTemplate(Guid id, string name, List<StepTemplate> steps)
    {
        ArgumentNullException.ThrowIfNull(steps);
        
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(id));
        }
        
        SetName(name);
        _steps = steps;
    }

    public WorkflowTemplate Create(string name)
    {
        var id = Guid.NewGuid();
        var steps = new List<StepTemplate>();
        
        return new WorkflowTemplate(id, name, steps);
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxNameLength} символов.", nameof(name));
        }
        
        Name = name.Trim();
    }

    // TODO
    // public Request Create(User user, Document document) => Request.Create(document, Workflow.Create(), user.Id);
}