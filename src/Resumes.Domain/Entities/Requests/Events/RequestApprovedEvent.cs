using Resumes.Domain.Entities.Exceptions;

namespace Resumes.Domain.Entities.Requests.Events;

public class RequestApprovedEvent : IEvent
{
    public Guid Id { get; }
    public DateTime Date { get; }
    public Guid RequestId { get; }

    private RequestApprovedEvent(Guid id, Guid requestId)
    {
        EmptyGuidException.ThrowIfEmpty(id);
        EmptyGuidException.ThrowIfEmpty(requestId);

        Id = id;
        Date = DateTime.UtcNow;
        RequestId = requestId;
    }
    
    public static RequestApprovedEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        return new RequestApprovedEvent(id, requestId);
    }
}