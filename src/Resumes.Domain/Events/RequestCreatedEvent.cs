using Resumes.Domain.Exceptions;

namespace Resumes.Domain.Events;

public class RequestCreatedEvent : IEvent
{
    public Guid Id { get; }
    public DateTime Date { get; }
    public Guid RequestId { get; }

    private RequestCreatedEvent(Guid id, Guid requestId)
    {
        EmptyGuidException.ThrowIfEmpty(id);
        EmptyGuidException.ThrowIfEmpty(requestId);

        Id = id;
        Date = DateTime.UtcNow;
        RequestId = requestId;
    }
    
    public static RequestCreatedEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        return new RequestCreatedEvent(id, requestId);
    }
}