using Ardalis.GuardClauses;
using Interviews.Domain.Tools;

namespace Interviews.Domain.Entities.Requests.Events;

public abstract record RequestEvent
{
    public Guid Id { get; private init;  }
    public DateTime DateTime { get; private init; }
    public Guid RequestId { get; private init; }

    protected RequestEvent(Guid id, DateTime dateTime, Guid requestId)
    {
        Guard.Against.GuidIsEmpty(id);
        Guard.Against.GuidIsEmpty(requestId);

        Id = id;
        DateTime = dateTime;
        RequestId = requestId;
    }
}