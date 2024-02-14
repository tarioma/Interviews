﻿namespace Interviews.Domain.Entities.WorkflowTemplates;

public record WorkflowStepTemplate
{
    private const int MaxNameLength = 100;

    public string Name { get; private init; }
    public int Order { get; private init; }
    public Guid EmployeeId { get; private init; }
    public Guid RoleId { get; private init; }

    public WorkflowStepTemplate(string name, int order, Guid employeeId, Guid roleId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxNameLength} символов.", nameof(name));
        }

        if (order < 0)
        {
            throw new ArgumentException("Не может быть отрицательным.", nameof(order));
        }

        if (employeeId == Guid.Empty && roleId == Guid.Empty)
        {
            throw new ArgumentException($"{nameof(employeeId)} и {nameof(roleId)} не могут одновременно быть пустыми.");
        }

        if (employeeId != Guid.Empty && roleId != Guid.Empty)
        {
            throw new ArgumentException($"Можно назначить только {nameof(employeeId)} или {nameof(roleId)}.");
        }

        Name = name.Trim();
        Order = order;
        EmployeeId = employeeId;
        RoleId = roleId;
    }
}