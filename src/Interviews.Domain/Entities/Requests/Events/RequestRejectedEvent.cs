namespace Interviews.Domain.Entities.Requests.Events;

public class RequestRejectedEvent(Guid id, DateTime dateTime, Guid requestId)
    : RequestEvent(id, dateTime, requestId)
{
    internal static RequestRejectedEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        var utcDateTime = DateTime.UtcNow;

        return new RequestRejectedEvent(id, utcDateTime, requestId);
    }
}