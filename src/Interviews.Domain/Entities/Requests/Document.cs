namespace Interviews.Domain.Entities.Requests;

public record Document
{
    private const int MaxNameLength = 100;
    private const int MinAcceptableAge = 18;
    
    public string Name { get; private init; }
    public DateOnly DateOfBirth { get; private init; }
    public EmailAddress EmailAddress { get; private init; }

    public Document(string name, DateOnly dateOfBirth, EmailAddress emailAddress)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentNullException.ThrowIfNull(emailAddress);
        
        if (name.Length > MaxNameLength)
        {
            throw new ArgumentException($"Максимальная длина {MaxNameLength} символов.", nameof(name));
        }

        if (!IsAgeAcceptable(dateOfBirth))
        {
            throw new ArgumentException($"Минимальный допустимый возраст {MinAcceptableAge} лет.", nameof(name));
        }

        Name = name.Trim();
        DateOfBirth = dateOfBirth;
        EmailAddress = emailAddress;
    }

    private static bool IsAgeAcceptable(DateOnly dateOfBirth)
    {
        var currentYear = DateTime.UtcNow.Year;
        var age = currentYear - dateOfBirth.Year;

        return age >= MinAcceptableAge;
    }
}