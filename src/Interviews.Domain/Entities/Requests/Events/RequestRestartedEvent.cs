namespace Interviews.Domain.Entities.Requests.Events;

public record RequestRestartedEvent : RequestEvent
{
    public RequestRestartedEvent(Guid id, DateTime dateTime, Guid requestId)
        : base(id, dateTime, requestId)
    {
    }

    internal static RequestRestartedEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        var utcDateTime = DateTime.UtcNow;

        return new RequestRestartedEvent(id, utcDateTime, requestId);
    }
}