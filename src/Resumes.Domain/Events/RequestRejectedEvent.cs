using Resumes.Domain.Exceptions;

namespace Resumes.Domain.Events;

public class RequestRejectedEvent : IEvent
{
    public Guid Id { get; }
    public DateTime Date { get; }
    public Guid RequestId { get; }

    private RequestRejectedEvent(Guid id, Guid requestId)
    {
        EmptyGuidException.ThrowIfEmpty(id);
        EmptyGuidException.ThrowIfEmpty(requestId);

        Id = id;
        Date = DateTime.UtcNow;
        RequestId = requestId;
    }
    
    public static RequestRejectedEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        return new RequestRejectedEvent(id, requestId);
    }
}