namespace Resumes.Domain.Events;

public interface IEvent
{
    Guid Id { get; }
    string Data { get; }
    Guid RequestId { get; }
}