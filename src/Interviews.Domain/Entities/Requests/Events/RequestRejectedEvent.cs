namespace Interviews.Domain.Entities.Requests.Events;

public class RequestRejectedEvent : RequestEvent
{
    public RequestRejectedEvent(Guid id, DateTime dateTime, Guid requestId)
        : base(id, dateTime, requestId)
    {
    }

    internal static RequestRejectedEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        var utcDateTime = DateTime.UtcNow;

        return new RequestRejectedEvent(id, utcDateTime, requestId);
    }
}