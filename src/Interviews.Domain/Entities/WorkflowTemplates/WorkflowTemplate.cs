using System.Diagnostics.CodeAnalysis;

namespace Interviews.Domain.Entities.WorkflowTemplates;

public class WorkflowTemplate
{
    private const int MaxNameLength = 100;
    
    private List<WorkflowStepTemplate> _steps;
    
    public Guid Id { get; private init; }
    public string Name { get; private set; }
    public IReadOnlyCollection<WorkflowStepTemplate> Steps => _steps;

    private WorkflowTemplate(Guid id, string name, IEnumerable<WorkflowStepTemplate> steps)
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
    
    public static WorkflowTemplate Create(string name, IEnumerable<WorkflowStepTemplate> steps)
    {
        var id = Guid.NewGuid();
        
        return new WorkflowTemplate(id, name, steps);
    }

    [MemberNotNull(nameof(Name))]
    private void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxNameLength} символов.", nameof(name));
        }
        
        Name = name.Trim();
    }
    
    [MemberNotNull(nameof(_steps))]
    private void SetSteps(IEnumerable<WorkflowStepTemplate> steps)
    {
        ArgumentNullException.ThrowIfNull(steps);
        
        steps = steps.ToArray();
        
        // Являются ли номера шагов уникальной порядковой последовательностью от 0 до их количества 
        var isCorrectOrder = steps
            .OrderBy(s => s.Order)
            .Select(s => s.Order)
            .SequenceEqual(Enumerable.Range(0, steps.Count()));
        
        if (!isCorrectOrder)
        {
            throw new ArgumentException("Номера шагов должны быть уникальной порядковой последовательностью от 0.",
                nameof(steps));
        }
 
        _steps = steps.ToList();
    }

    // TODO
    // public Request Create(Employee user, Document document) => Request.Create(document, Workflow.Create(), user.Id);
}