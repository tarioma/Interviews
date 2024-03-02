using Ardalis.GuardClauses;

namespace Interviews.Domain.Entities.Requests.Events;

public abstract class RequestEvent
{
    protected RequestEvent(Guid id, DateTime dateTime, Guid requestId)
    {
        Guard.Against.Default(id);
        Guard.Against.Default(dateTime);
        Guard.Against.Default(requestId);

        Id = id;
        DateTime = dateTime;
        RequestId = requestId;
    }

    public Guid Id { get; }
    public DateTime DateTime { get; }
    public Guid RequestId { get; }
}