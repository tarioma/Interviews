namespace Interviews.Domain.Entities;

public record EmailAddress
{
    public const int MaxValueLength = 100;
    
    public string Value { get; private init; }

    public EmailAddress(string value)
    {
        value = value.Trim();

        if (value.Length > MaxValueLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxValueLength} символов.", nameof(value));
        }
        
        if (!IsEmail(value))
        {
            throw new ArgumentException("Адрес навалиден.", nameof(value));
        }

        Value = value.ToUpperInvariant();
    }

    private static bool IsEmail(string value) => value.Contains('@') &&
                                                 !value.StartsWith('@') &&
                                                 !value.EndsWith('@');
}