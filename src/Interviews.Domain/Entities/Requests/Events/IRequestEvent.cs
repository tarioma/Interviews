namespace Interviews.Domain.Entities.Requests.Events;

public interface IRequestEvent
{
    Guid Id { get; }
    DateTime DateTime { get; }
    Guid RequestId { get; }
}