﻿namespace Interviews.Domain.Entities.Requests.Events;

public record RequestRejectedEvent : IRequestEvent
{
    public Guid Id { get; private init; }
    public DateTime UtcDateTime { get; private init; }
    public Guid RequestId { get; private init; }

    private RequestRejectedEvent(Guid id, DateTime utcDateTime, Guid requestId)
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
        UtcDateTime = utcDateTime;
        RequestId = requestId;
    }

    public static RequestRejectedEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        var utcDateTime = DateTime.UtcNow;

        return new RequestRejectedEvent(id, utcDateTime, requestId);
    }
}