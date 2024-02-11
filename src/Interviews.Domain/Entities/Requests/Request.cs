using System.Diagnostics.CodeAnalysis;
using Interviews.Domain.Entities.Requests.Events;
using Interviews.Domain.Entities.Users;

namespace Interviews.Domain.Entities.Requests;

public class Request
{
    private readonly List<IRequestEvent> _events;

    public Guid Id { get; private init; }
    public Document Document { get; private set; }
    public Workflow Workflow { get; private set; }
    public Guid EmployeeId { get; private set; }
    public IReadOnlyCollection<IRequestEvent> Events => _events;

    private Request(Guid id, Document document, Workflow workflow, Guid employeeId)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(id));
        }

        Id = id;
        SetDocument(document);
        SetWorkflow(workflow);
        SetEmployeeId(employeeId);
        _events = new List<IRequestEvent>();
    }

    public static Request Create(Document document, Workflow workflow, Guid employeeId)
    {
        var id = Guid.NewGuid();
        var request = new Request(id, document, workflow, employeeId);

        var requestCreatedEvent = RequestCreatedEvent.Create(request.Id);
        request._events.Add(requestCreatedEvent);

        return request;
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

    private void SetEmployeeId(Guid employeeId)
    {
        if (employeeId == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(employeeId));
        }

        EmployeeId = employeeId;
    }

    public bool IsApproved() => Workflow.IsApproved();

    public bool IsRejected() => Workflow.IsRejected();

    public void Approve(Employee employee, string? comment = null)
    {
        _events.Add(RequestApprovedEvent.Create(Id));
        Workflow.Approve(employee, comment);
    }

    public void Reject(Employee employee, string? comment = null)
    {
        _events.Add(RequestRejectedEvent.Create(Id));
        Workflow.Reject(employee, comment);
    }
    
    public void Restart(Employee employee)
    {
        _events.Add(RequestRestartedEvent.Create(Id));
        Workflow.Restart(employee);
    }
}