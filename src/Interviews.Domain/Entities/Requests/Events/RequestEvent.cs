using Ardalis.GuardClauses;
using Interviews.Domain.Tools;

namespace Interviews.Domain.Entities.Requests.Events;

public abstract record RequestEvent
{
    protected RequestEvent(Guid id, DateTime dateTime, Guid requestId)
    {
        Guard.Against.GuidIsEmpty(id);
        Guard.Against.GuidIsEmpty(requestId);

        Id = id;
        DateTime = dateTime;
        RequestId = requestId;
    }
    
    public Guid Id { get; }
    public DateTime DateTime { get; }
    public Guid RequestId { get; }
}