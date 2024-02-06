using Interviews.Domain.Entities.Requests.Events;

namespace Interviews.Domain.Entities.Requests;

public class Request
{
    private readonly List<IRequestEvent> _events;
    
    public Guid Id { get; private init; }
    public Document Document { get; private set; } = null!;
    public Workflow Workflow { get; private set; } = null!;
    public Guid UserId { get; private set; }
    public IReadOnlyCollection<IRequestEvent> Events => _events;

    private Request(Guid id, Document document, Workflow workflow, Guid userId, List<IRequestEvent> events)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(id));
        }

        Id = id;
        SetDocument(document);
        SetWorkflow(workflow);
        SetUserId(userId);
        _events = events;
    }

    public static Request Create(Document document, Workflow workflow, Guid userId)
    {
        var id = Guid.NewGuid();
        var events = new List<IRequestEvent>();

        return new Request(id, document, workflow, userId, events);
    }

    public void SetDocument(Document document)
    {
        ArgumentNullException.ThrowIfNull(document);

        Document = document;
    }
    
    public void SetWorkflow(Workflow workflow)
    {
        ArgumentNullException.ThrowIfNull(workflow);

        Workflow = workflow;
    }
    
    public void SetUserId(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(userId));
        }

        UserId = userId;
    }

    public bool IsApproved() => Events.Last() is RequestApprovedEvent;
    
    public bool IsRejected() => Events.Last() is RequestRejectedEvent;
    
    public void Approve()
    {
        var approvedEvent = RequestApprovedEvent.Create(Id);
        _events.Add(approvedEvent);
    }
    
    public void Reject()
    {
        var rejectedEvent = RequestRejectedEvent.Create(Id);
        _events.Add(rejectedEvent);
    }

    public void Restart() => _events.Clear();
}