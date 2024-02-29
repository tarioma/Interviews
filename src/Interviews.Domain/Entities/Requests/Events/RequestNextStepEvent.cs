﻿using System.Diagnostics.CodeAnalysis;

namespace Interviews.Domain.Entities.Requests.Events;

[ExcludeFromCodeCoverage]
public record RequestNextStepEvent : RequestEvent
{
    public RequestNextStepEvent(Guid id, DateTime dateTime, Guid requestId)
        : base(id, dateTime, requestId)
    {
    }

    internal static RequestNextStepEvent Create(Guid requestId)
    {
        var id = Guid.NewGuid();
        var utcDateTime = DateTime.UtcNow;

        return new RequestNextStepEvent(id, utcDateTime, requestId);
    }
}