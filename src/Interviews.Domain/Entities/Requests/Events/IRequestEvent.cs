namespace Interviews.Domain.Entities.Requests.Events;

public interface IRequestEvent
{
    Guid Id { get; }
    DateTime UtcDateTime { get; }
    Guid RequestId { get; }
}