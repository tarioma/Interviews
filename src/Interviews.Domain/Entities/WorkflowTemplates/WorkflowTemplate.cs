﻿using Ardalis.GuardClauses;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.Requests;

namespace Interviews.Domain.Entities.WorkflowTemplates;

public class WorkflowTemplate
{
    internal const int MaxNameLength = 100;

    internal WorkflowTemplate(Guid id, string name, IReadOnlyCollection<WorkflowStepTemplate> steps)
    {
        Guard.Against.Default(id);
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.StringTooLong(name, MaxNameLength);

        ValidateSteps(steps);

        Id = id;
        Name = name;
        Steps = steps;
    }

    public Guid Id { get; }
    public string Name { get; }
    public IReadOnlyCollection<WorkflowStepTemplate> Steps { get; }

    public static WorkflowTemplate Create(string name, IReadOnlyCollection<WorkflowStepTemplate> steps)
    {
        var id = Guid.NewGuid();

        return new WorkflowTemplate(id, name, steps);
    }

    public Request CreateRequest(Employee employee, Document document)
    {
        var workflow = Workflow.Create(this);

        return Request.Create(document, workflow, employee.Id);
    }

    private static void ValidateSteps(IReadOnlyCollection<WorkflowStepTemplate> steps)
    {
        Guard.Against.NullOrEmpty(steps);

        // Являются ли номера шагов уникальной возрастающей порядковой последовательностью от 0 до их количества 
        var isCorrectOrder = steps
            .OrderBy(s => s.Order)
            .Select(s => s.Order)
            .SequenceEqual(Enumerable.Range(0, steps.Count));

        if (!isCorrectOrder)
        {
            throw new ArgumentException(
                "Номера шагов должны быть уникальной порядковой возрастающей последовательностью от 0.",
                nameof(steps));
        }
    }
}