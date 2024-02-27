﻿using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using GuardClauses;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.Requests;
using Interviews.Domain.Tools;

namespace Interviews.Domain.Entities.WorkflowTemplates;

public class WorkflowTemplate
{
    public const int MaxNameLength = 100;

    public WorkflowTemplate(Guid id, string name, IEnumerable<WorkflowStepTemplate> steps)
    {
        Guard.Against.GuidIsEmpty(id);
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);

        var stepsList = steps.ToList();
        ValidateSteps(stepsList);

        Id = id;
        SetName(name);
        Steps = stepsList;
    }
    
    public Guid Id { get; }
    public string Name { get; private set; }
    public IReadOnlyCollection<WorkflowStepTemplate> Steps { get; }
    
    public static WorkflowTemplate Create(string name, IEnumerable<WorkflowStepTemplate> steps)
    {
        var id = Guid.NewGuid();
        
        return new WorkflowTemplate(id, name, steps);
    }

    public Request CreateRequest(Employee employee, Document document)
    {
        var workflow = Workflow.Create(this);
        return Request.Create(document, workflow, employee.Id);
    }

    [MemberNotNull(nameof(Name))]
    private void SetName(string name)
    {
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);
        
        Name = name.Trim();
    }
    
    private static void ValidateSteps(List<WorkflowStepTemplate> steps)
    {
        Guard.Against.NullOrEmpty(steps);
        
        // Являются ли номера шагов уникальной возрастающей порядковой последовательностью от 0 до их количества 
        var isCorrectOrder = steps
            .OrderBy(s => s.Order)
            .Select(s => s.Order)
            .SequenceEqual(Enumerable.Range(0, steps.Count));
        
        if (!isCorrectOrder)
        {
            throw new ArgumentException("Номера шагов должны быть уникальной порядковой последовательностью от 0.",
                nameof(steps));
        }
    }
}