namespace Interviews.Domain.Entities.Requests.Events;

public class RequestCreatedEvent : RequestEvent
{
    public RequestCreatedEvent(Guid id, DateTime dateTime, Guid requestId)
        : base(id, dateTime, requestId)
    {
    }

    internal static RequestCreatedEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        var utcDateTime = DateTime.UtcNow;

        return new RequestCreatedEvent(id, utcDateTime, requestId);
    }
}