using Resumes.Domain.Events;
using Resumes.Domain.Exceptions;

namespace Resumes.Domain.InterviewRequest;

public class Request
{
    private readonly HashSet<IEvent> _events;
    
    public Guid Id { get; }
    public Guid UserId { get; private set; }
    public Document Document { get; private set; } = null!;
    public Workflow Workflow { get; private set; } = null!;
    public IReadOnlyCollection<IEvent> Events => _events;

    private Request(Guid id, Guid userId, Document document, Workflow workflow)
    {
        EmptyGuidException.ThrowIfEmpty(id);

        _events = new HashSet<IEvent>();
        
        Id = id;
        SetUserId(userId);
        SetDocument(document);
        SetWorkflowRequest(workflow);
    }

    public static Request Create(Guid userId, Document document, Workflow workflow)
    {
        var id = Guid.NewGuid();
        return new Request(id, userId, document, workflow);
    }

    public bool IsApproved() => Events.Count != 0 && Events.Last() is RequestApprovedEvent;
    public bool IsRejected() => Events.Count != 0 && Events.Last() is RequestRejectedEvent;

    public void Approve()
    {
        var event_ = RequestApprovedEvent.Create(Id);
        _events.Add(event_);
    }
    
    public void Reject()
    {
        var event_ = RequestRejectedEvent.Create(Id);
        _events.Add(event_);
    }
    
    public void Restart()
    {
        var event_ = RequestCreatedEvent.Create(Id);
        _events.Add(event_);
    }

    private void SetUserId(Guid userId)
    {
        EmptyGuidException.ThrowIfEmpty(userId);
        UserId = userId;
    }
    
    private void SetDocument(Document document)
    {
        ArgumentNullException.ThrowIfNull(document);
        Document = document;
    }
    
    private void SetWorkflowRequest(Workflow workflow)
    {
        ArgumentNullException.ThrowIfNull(workflow);
        Workflow = workflow;
    }
}