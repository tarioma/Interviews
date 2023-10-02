using Resumes.Domain.Events;
using Resumes.Domain.Exceptions;

namespace Resumes.Domain.InterviewRequest;

public class Request
{
    public Guid Id { get; private init; }
    public Guid UserId { get; private set; }
    public Document Document { get; private set; }
    public WorkflowRequest WorkflowRequest { get; private set; }
    public HashSet<IEvent> Events { get; private init; }

    private Request(
        Guid id,
        Guid userId,
        Document document,
        WorkflowRequest workflowRequest,
        HashSet<IEvent> events)
    {
        EmptyGuidException.ThrowIfEmpty(id);
        EmptyGuidException.ThrowIfEmpty(userId);
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(workflowRequest);
        ArgumentNullException.ThrowIfNull(events);

        Id = id;
        UserId = userId;
        Document = document;
        WorkflowRequest = workflowRequest;
        Events = events;
    }

    public static Request Create(Guid userId, Document document, WorkflowRequest workflowRequest)
    {
        EmptyGuidException.ThrowIfEmpty(userId);
        ArgumentNullException.ThrowIfNull(document);
        ArgumentNullException.ThrowIfNull(workflowRequest);

        var id = Guid.NewGuid();
        var events = new HashSet<IEvent>();
        return new Request(id, userId, document, workflowRequest, events);
    }

    public bool IsApproved()
    {
        return Events.Count != 0 &&
               Events.Last() is RequestApprovedEvent;
    }
    
    public bool IsRejected()
    {
        return Events.Count != 0 &&
               Events.Last() is RequestRejectedEvent;
    }

    public void Approve()
    {
        var event_ = RequestApprovedEvent.Create("Approve", Id);
        Events.Add(event_);
    }
    
    public void Reject()
    {
        var event_ = RequestRejectedEvent.Create("Reject", Id);
        Events.Add(event_);
    }
    
    public void Restart()
    {
        var event_ = RequestCreatedEvent.Create("Restart", Id);
        Events.Add(event_);
    }
}