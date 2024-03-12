namespace Interviews.Domain.Entities.Requests.Events;

public class RequestNextStepEvent(Guid id, DateTime dateTime, Guid requestId)
    : RequestEvent(id, dateTime, requestId)
{
    internal static RequestNextStepEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        var utcDateTime = DateTime.UtcNow;

        return new RequestNextStepEvent(id, utcDateTime, requestId);
    }
}