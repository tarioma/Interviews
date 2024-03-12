namespace Interviews.Domain.Entities.Requests.Events;

public class RequestCreatedEvent(Guid id, DateTime dateTime, Guid requestId)
    : RequestEvent(id, dateTime, requestId)
{
    internal static RequestCreatedEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        var utcDateTime = DateTime.UtcNow;

        return new RequestCreatedEvent(id, utcDateTime, requestId);
    }
}