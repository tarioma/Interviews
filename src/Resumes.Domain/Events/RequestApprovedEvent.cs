using Resumes.Domain.Exceptions;

namespace Resumes.Domain.Events;

public class RequestApprovedEvent : IEvent
{
    public Guid Id { get; private init; }
    public string Data { get; private init; }
    public Guid RequestId { get; private init; }

    private RequestApprovedEvent(Guid id, string data, Guid requestId)
    {
        EmptyGuidException.ThrowIfEmpty(id);
        ArgumentException.ThrowIfNullOrEmpty(data);
        EmptyGuidException.ThrowIfEmpty(requestId);

        Id = id;
        Data = data;
        RequestId = requestId;
    }
    
    public static RequestApprovedEvent Create(string data, Guid requestId)
    {
        ArgumentException.ThrowIfNullOrEmpty(data);
        EmptyGuidException.ThrowIfEmpty(requestId);

        var id = Guid.NewGuid();
        return new RequestApprovedEvent(id, data, requestId);
    }
}