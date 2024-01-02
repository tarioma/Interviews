namespace Resumes.Domain.Events;

public interface IEvent
{
    Guid Id { get; }
    DateTime Date { get; }
    Guid RequestId { get; }
}