using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.Requests.Events;

namespace Interviews.Domain.Entities.Requests;

public class Request
{
    private readonly List<RequestEvent> _events;

    internal Request(Guid id, Document document, Workflow workflow, Guid employeeId)
    {
        Guard.Against.Default(id);
        Guard.Against.Null(workflow);
        Guard.Against.Default(employeeId);

        Id = id;
        SetDocument(document);
        Workflow = workflow;
        EmployeeId = employeeId;
        _events = new List<RequestEvent>();
    }

    public Guid Id { get; }
    public Document Document { get; private set; }
    public Workflow Workflow { get; }
    public Guid EmployeeId { get; }
    public IReadOnlyCollection<RequestEvent> Events => _events;

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
        Workflow.Restart();
        SetDocument(document);
        _events.Add(RequestRestartedEvent.Create(Id));
    }

    [MemberNotNull(nameof(Document))]
    private void SetDocument(Document document)
    {
        Guard.Against.Null(document);

        Document = document;
    }
}