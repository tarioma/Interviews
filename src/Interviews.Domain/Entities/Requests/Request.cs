﻿using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.Requests.Events;
using Interviews.Domain.Tools;

namespace Interviews.Domain.Entities.Requests;

public class Request
{
    private readonly List<RequestEvent> _events;

    public Guid Id { get; private init; }
    public Document Document { get; private set; }
    public Workflow Workflow { get; private set; }
    public Guid EmployeeId { get; private init; }
    public IReadOnlyCollection<RequestEvent> Events => _events;

    public Request(Guid id, Document document, Workflow workflow, Guid employeeId)
    {
        Guard.Against.GuidIsEmpty(id);
        Guard.Against.Null(document);
        Guard.Against.Null(workflow);
        Guard.Against.GuidIsEmpty(employeeId);

        Id = id;
        Document = document;
        SetWorkflow(workflow);
        EmployeeId = employeeId;
        _events = new List<RequestEvent>();
    }

    public static Request Create(Document document, Workflow workflow, Guid employeeId)
    {
        var id = Guid.NewGuid();
        var request = new Request(id, document, workflow, employeeId);

        var requestCreatedEvent = RequestCreatedEvent.Create(request.Id);
        request._events.Add(requestCreatedEvent);

        return request;
    }

    public bool IsApproved() => Workflow.IsApproved();

    public bool IsRejected() => Workflow.IsRejected();

    public void Approve(Employee employee, string? comment = null)
    {
        Workflow.Approve(employee, comment);

        _events.Add(IsApproved()
            ? RequestApprovedEvent.Create(Id)
            : RequestNextStepEvent.Create(Id));
    }

    public void Reject(Employee employee, string? comment = null)
    {
        Workflow.Reject(employee, comment);
        _events.Add(RequestRejectedEvent.Create(Id));
    }

    public void Restart(Document document)
    {
        SetDocument(document);
        Workflow.Restart();
        _events.Add(RequestRestartedEvent.Create(Id));
    }

    [MemberNotNull(nameof(Document))]
    private void SetDocument(Document document)
    {
        Guard.Against.Null(document);

        Document = document;
    }

    [MemberNotNull(nameof(Workflow))]
    private void SetWorkflow(Workflow workflow)
    {
        Guard.Against.Null(workflow);

        Workflow = workflow;
    }
}