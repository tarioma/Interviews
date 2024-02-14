using System.Diagnostics.CodeAnalysis;
using Interviews.Domain.Entities.Employees;
using Interviews.Domain.Entities.Requests.Events;

namespace Interviews.Domain.Entities.Requests;

public class Request
{
    private readonly List<IRequestEvent> _events;

    public Guid Id { get; private init; }
    public Document Document { get; private set; }
    public Workflow Workflow { get; private set; }
    public Guid EmployeeId { get; private init; }
    public IReadOnlyCollection<IRequestEvent> Events => _events;

    private Request(Guid id, Document document, Workflow workflow, Guid employeeId)
    {
        ArgumentNullException.ThrowIfNull(document);

        if (id == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(id));
        }

        if (employeeId == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(employeeId));
        }

        Id = id;
        Document = document;
        EmployeeId = employeeId;
        _events = new List<IRequestEvent>();

        SetWorkflow(workflow);
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

    public void Restart(Employee employee, Document document)
    {
        SetDocument(document);
        Workflow.Restart(employee);
        _events.Add(RequestRestartedEvent.Create(Id));
    }

    [MemberNotNull(nameof(Document))]
    private void SetDocument(Document document)
    {
        ArgumentNullException.ThrowIfNull(document);

        Document = document;
    }

    [MemberNotNull(nameof(Workflow))]
    private void SetWorkflow(Workflow workflow)
    {
        ArgumentNullException.ThrowIfNull(workflow);

        Workflow = workflow;
    }
}