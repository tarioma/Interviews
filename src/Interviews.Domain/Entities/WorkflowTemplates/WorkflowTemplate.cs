using System.Diagnostics.CodeAnalysis;

namespace Interviews.Domain.Entities.WorkflowTemplates;

public class WorkflowTemplate
{
    private const int MaxNameLength = 100;
    
    private List<StepTemplate> _steps;
    
    public Guid Id { get; private init; }
    public string Name { get; private set; }
    public IReadOnlyCollection<StepTemplate> Steps => _steps;

    private WorkflowTemplate(Guid id, string name, IEnumerable<StepTemplate> steps)
    {
        ArgumentNullException.ThrowIfNull(steps);
        
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(id));
        }

        Id = id;
        SetName(name);
        SetSteps(steps);
    }
    
    public static WorkflowTemplate Create(string name)
    {
        var id = Guid.NewGuid();
        var steps = new List<StepTemplate>();
        
        return new WorkflowTemplate(id, name, steps);
    }

    [MemberNotNull(nameof(Name))]
    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxNameLength} символов.", nameof(name));
        }
        
        Name = name.Trim();
    }
    
    [MemberNotNull(nameof(_steps))]
    private void SetSteps(IEnumerable<StepTemplate> steps)
    {
        ArgumentNullException.ThrowIfNull(steps);

        var stepsList = steps.ToList();
        var uniqueOrdersCount = stepsList.GroupBy(s => s.Order).Count();

        if (uniqueOrdersCount < stepsList.Count)
        {
            throw new ArgumentException("Номера шагов должны быть уникальными.", nameof(steps));
        }
            
        var orderedSteps = stepsList.OrderBy(s => s.Order);
        _steps = orderedSteps.ToList();
    }

    // TODO
    // public Request Create(User user, Document document) => Request.Create(document, Workflow.Create(), user.Id);
}