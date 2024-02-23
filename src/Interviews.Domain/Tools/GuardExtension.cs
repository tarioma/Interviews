using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;

namespace Interviews.Domain.Tools;

public static class GuardExtension
{
    public static Guid GuidIsEmpty(
        this IGuardClause _,
        Guid guid,
        [CallerArgumentExpression("guid")] string? parameterName = null)
    {
        if (guid == Guid.Empty)
        {
            throw new ArgumentException("Не может быть пустым.", parameterName);
        }
        
        return guid;
    }
    
    public static T EnumWithDefaultValue<T>(
        this IGuardClause _,
        T value,
        [CallerArgumentExpression("value")] string? parameterName = null) where T : Enum
    {
        if (EqualityComparer<T>.Default.Equals(value, default))
        {
            throw new ArgumentException("Не может иметь значение по умолчанию.", parameterName);
        }
        
        return value;
    }
}