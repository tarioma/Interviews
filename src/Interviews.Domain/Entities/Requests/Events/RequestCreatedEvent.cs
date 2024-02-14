namespace Interviews.Domain.Entities.Requests.Events;

public record RequestCreatedEvent : IRequestEvent
{
    public Guid Id { get; private init; }
    public DateTime DateTime { get; private init; }
    public Guid RequestId { get; private init; }

    private RequestCreatedEvent(Guid id, DateTime dateTime, Guid requestId)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(id));
        }
        
        if (requestId == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(requestId));
        }

        Id = id;
        DateTime = dateTime;
        RequestId = requestId;
    }

    public static RequestCreatedEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        var dateTime = DateTime.UtcNow;

        return new RequestCreatedEvent(id, dateTime, requestId);
    }
}