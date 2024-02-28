namespace Interviews.Domain.Entities.Requests.Events;

public record RequestApprovedEvent : RequestEvent
{
    public RequestApprovedEvent(Guid id, DateTime dateTime, Guid requestId)
        : base(id, dateTime, requestId)
    {
    }

    internal static RequestApprovedEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        var utcDateTime = DateTime.UtcNow;

        return new RequestApprovedEvent(id, utcDateTime, requestId);
    }
}