namespace Interviews.Domain.Entities.Requests.Events;

public abstract record RequestEvent
{
    public Guid Id { get; private init;  }
    public DateTime DateTime { get; private init; }
    public Guid RequestId { get; private init; }

    protected RequestEvent(Guid id, DateTime dateTime, Guid requestId)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(id));
        }
        
        if (requestId == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(requestId));
        }

        Id = id;
        DateTime = dateTime;
        RequestId = requestId;
    }
}