namespace Interviews.Domain.Entities.Requests.Events;

public class RequestRestartedEvent(Guid id, DateTime dateTime, Guid requestId)
    : RequestEvent(id, dateTime, requestId)
{
    internal static RequestRestartedEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        var utcDateTime = DateTime.UtcNow;

        return new RequestRestartedEvent(id, utcDateTime, requestId);
    }
}