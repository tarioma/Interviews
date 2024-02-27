using Ardalis.GuardClauses;
using GuardClauses;

namespace Interviews.Domain.Entities;

public record EmailAddress
{
    public const int MaxValueLength = 100;

    public EmailAddress(string value)
    {
        Guard.Against.Null(value);
        Guard.Against.StringTooLong(value, MaxValueLength);

        value = value.Trim();
        
        if (!IsEmail(value))
        {
            throw new ArgumentException("Адрес навалиден.", nameof(value));
        }

        Value = value.ToUpperInvariant();
    }
    
    public string Value { get; }

    private static bool IsEmail(string value) => value.Contains('@') &&
                                                 !value.StartsWith('@') &&
                                                 !value.EndsWith('@');
}