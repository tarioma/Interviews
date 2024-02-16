using System.Diagnostics.CodeAnalysis;

namespace Interviews.Domain.Entities.Employees;

/// <summary>
/// Представляет собой сторудника компании.
/// </summary>
public class Employee
{
    private const int MaxNameLength = 100;
    
    /// <summary>
    /// Уникальный идентификатор сотрудника.
    /// </summary>
    public Guid Id { get; private init; }
    
    /// <summary>
    /// Имя сотрудника.
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Email-адрес сотрудника.
    /// </summary>
    public EmailAddress EmailAddress { get; private set; }
    
    /// <summary>
    /// Идентификатор должности сотрудника.
    /// </summary>
    public Guid RoleId { get; private set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="Employee"/>.
    /// </summary>
    /// <param name="id">Уникальный идентификатор сотрудника.</param>
    /// <param name="name">Имя сотрудника.</param>
    /// <param name="emailAddress">Email-адрес сотрудника.</param>
    /// <param name="roleId">Идентификатор должности сотрудника.</param>
    private Employee(Guid id, string name, EmailAddress emailAddress, Guid roleId)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(id));
        }

        Id = id;
        SetName(name);
        SetEmailAddress(emailAddress);
        SetRoleId(roleId);
    }
    
    /// <summary>
    /// Инициализирует и возвращает новый экземпляр класса <see cref="Employee"/>.
    /// </summary>
    /// <remarks>
    /// Генерирует <see cref="Id"/> для создаваемого экземпляра класса.
    /// </remarks>
    /// <param name="name">Имя сотрудника.</param>
    /// <param name="emailAddress">Email-адрес сотрудника.</param>
    /// <param name="roleId">Идентификатор должности сотрудника.</param>
    /// <returns>Экземпляр класса <see cref="Employee"/>.</returns>
    public static Employee Create(string name, EmailAddress emailAddress, Guid roleId)
    {
        var id = Guid.NewGuid();
        
        return new Employee(id, name, emailAddress, roleId);
    }

    /// <summary>
    /// Устанавливает имя сотрудника.
    /// </summary>
    /// <param name="name">Новое имя сотрудника.</param>
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
    
    /// <summary>
    /// Устанавливает email-адрес сотрудника.
    /// </summary>
    /// <param name="emailAddress">Новый email-адрес сотрудника.</param>
    [MemberNotNull(nameof(EmailAddress))]
    private void SetEmailAddress(EmailAddress emailAddress)
    {
        ArgumentNullException.ThrowIfNull(emailAddress);
        
        EmailAddress = emailAddress;
    }
    
    /// <summary>
    /// Устанавливает идентификатор должности сотрудника.
    /// </summary>
    /// <param name="roleId">Новый идентификатор должности сотрудника.</param>
    private void SetRoleId(Guid roleId)
    {
        if (roleId == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", nameof(roleId));
        }

        RoleId = roleId;
    }
}