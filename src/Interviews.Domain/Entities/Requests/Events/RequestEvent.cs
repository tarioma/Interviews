namespace Interviews.Domain.Entities.Requests.Events;

/// <summary>
/// Представляет собой событие, связанное с изменением статуса запроса.
/// </summary>
public abstract record RequestEvent
{
    /// <summary>
    /// Уникальный идентификатор события.
    /// </summary>
    public Guid Id { get; private init; }
    
    /// <summary>
    /// Дата и время события.
    /// </summary>
    public DateTime DateTime { get; private init; }
    
    /// <summary>
    /// Уникальный идентификатор запроса.
    /// </summary>
    public Guid RequestId { get; private init; }

    /// <summary>
    /// Инициализирует новый экземпляр записи <see cref="RequestEvent"/>.
    /// </summary>
    /// <param name="id">Уникальный идентификатор события.</param>
    /// <param name="dateTime">Дата и время события.</param>
    /// <param name="requestId">Уникальный идентификатор запроса.</param>
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