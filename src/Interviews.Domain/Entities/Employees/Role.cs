using System.Diagnostics.CodeAnalysis;

namespace Interviews.Domain.Entities.Employees;

/// <summary>
/// Представляет собой должность в компании.
/// </summary>
public class Role
{
    private const int MaxNameLength = 100;
    
    /// <summary>
    /// Уникальный идентификатор должности.
    /// </summary>
    public Guid Id { get; private init; }
    
    /// <summary>
    /// Название должности.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Role"/>.
    /// </summary>
    /// <param name="id">Уникальный идентификатор должности.</param>
    /// <param name="name">Название должности.</param>
    private Role(Guid id, string name)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(id));
        }

        Id = id;
        SetName(name);
    }

    /// <summary>
    /// Инициализирует и возвращает новый экземпляр класса <see cref="Role"/>.
    /// </summary>
    /// <remarks>
    /// Генерирует <see cref="Id"/> для создаваемого экземпляра класса.
    /// </remarks>
    /// <param name="name">Название должности.</param>
    /// <returns>Экземпляр класса <see cref="Role"/>.</returns>
    public static Role Create(string name)
    {
        var id = Guid.NewGuid();

        return new Role(id, name);
    }
    
    /// <summary>
    /// Устанавливает название должности.
    /// </summary>
    /// <param name="name">Новое название должности.</param>
    [MemberNotNull(nameof(Name))]
    private void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxNameLength} символов.", nameof(name));   
        }

        Name = name.Trim();
    }
}