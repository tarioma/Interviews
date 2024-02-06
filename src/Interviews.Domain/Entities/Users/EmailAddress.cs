using System.ComponentModel.DataAnnotations;

namespace Interviews.Domain.Entities.Users;

public record EmailAddress
{
    private const int MaxValueLength = 100;
    
    public string Value { get; private init; }

    public EmailAddress(string value)
    {
        if (value.Length > MaxValueLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxValueLength} символов.", nameof(value));
        }

        if (!IsEmail(value))
        {
            throw new ArgumentException("Адрес навалиден.", nameof(value));
        }
        
        Value = value.Trim();
    }

    private static bool IsEmail(string value) => new EmailAddressAttribute().IsValid(value);
}

