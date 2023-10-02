using Resumes.Domain.Exceptions;

namespace Resumes.Domain.Events;

public class RequestRejectedEvent : IEvent
{
    public Guid Id { get; private init; }
    public string Data { get; private init; }
    public Guid RequestId { get; private init; }

    private RequestRejectedEvent(Guid id, string data, Guid requestId)
    {
        EmptyGuidException.ThrowIfEmpty(id);
        ArgumentException.ThrowIfNullOrEmpty(data);
        EmptyGuidException.ThrowIfEmpty(requestId);

        Id = id;
        Data = data;
        RequestId = requestId;
    }
    
    public static RequestRejectedEvent Create(string data, Guid requestId)
    {
        ArgumentException.ThrowIfNullOrEmpty(data);
        EmptyGuidException.ThrowIfEmpty(requestId);

        var id = Guid.NewGuid();
        return new RequestRejectedEvent(id, data, requestId);
    }
}