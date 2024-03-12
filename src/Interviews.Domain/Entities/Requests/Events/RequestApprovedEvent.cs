namespace Interviews.Domain.Entities.Requests.Events;

public class RequestApprovedEvent(Guid id, DateTime dateTime, Guid requestId)
    : RequestEvent(id, dateTime, requestId)
{
    internal static RequestApprovedEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        var utcDateTime = DateTime.UtcNow;

        return new RequestApprovedEvent(id, utcDateTime, requestId);
    }
}